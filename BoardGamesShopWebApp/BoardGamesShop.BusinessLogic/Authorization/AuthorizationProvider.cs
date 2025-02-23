using AutoMapper;
using BoardGamesShop.BusinessLogic.Authorization.Entities;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.BusinessLogic.Users.Exceptions;
using BoardGamesShop.DataAccess.Entities;
using Duende.IdentityServer.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using TokenResponse = BoardGamesShop.BusinessLogic.Authorization.Entities.TokenResponse;

namespace BoardGamesShop.BusinessLogic.Authorization;

public class AuthorizationProvider : IAuthorizationProvider
{
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMapper _mapper;
    private readonly string _identityServerUri;
    private readonly string _clientId;
    private readonly string _clientSecret;

    public AuthorizationProvider(SignInManager<UserEntity> signInManager, 
        UserManager<UserEntity> userManager, 
        IHttpClientFactory httpClientFactory, 
        IMapper mapper, 
        string identityServerUri, 
        string clientId, 
        string clientSecret)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _httpClientFactory = httpClientFactory;
        _mapper = mapper;
        _identityServerUri = identityServerUri;
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    public async Task<UserModel> RegisterUserAsync(RegisterUserModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is not null)
        {
            throw new BusinessLogicException(ResultCode.UserAlreadyExists);
        }
        
        user = _mapper.Map<UserEntity>(model);
        user.ExternalId = Guid.NewGuid();
        var time = DateTime.UtcNow;
        user.CreationTime = time;
        user.ModificationTime = time;
        user.UserName = model.Email;
        user.Role = new UserRole(model.Role.ToString());
        
        var createResult = await _userManager.CreateAsync(user, model.Password);
        if (!createResult.Succeeded)
        {
            throw new BusinessLogicException(ResultCode.UserCreationFailure,
                string.Join(Environment.NewLine, createResult.Errors.Select(e => e.Description)));
        }
        
        return _mapper.Map<UserModel>(user);
    }

    public async Task<TokenResponse> LoginUserAsync(AuthorizeUserModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
        {
            throw new BusinessLogicException(ResultCode.UserNotFound);
        }
        
        var verificationResult = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!verificationResult.Succeeded)
        {
            throw new BusinessLogicException(ResultCode.EmailOrPasswordIsIncorrect);
        }
        
        var client = _httpClientFactory.CreateClient();
        var discoveryDocument = await client.GetDiscoveryDocumentAsync(_identityServerUri);
        if (discoveryDocument.IsError)
        {
            throw new BusinessLogicException(ResultCode.IdentityServerError);
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = discoveryDocument.TokenEndpoint,
            GrantType = GrantType.ResourceOwnerPassword,
            ClientId = _clientId,
            ClientSecret = _clientSecret,
            UserName = user.UserName,
            Password = model.Password,
            Scope = "api offline_access"
        });

        if (tokenResponse.IsError)
        {
            throw new BusinessLogicException(ResultCode.IdentityServerError);
        }
        
        return new TokenResponse
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        };
    }
}