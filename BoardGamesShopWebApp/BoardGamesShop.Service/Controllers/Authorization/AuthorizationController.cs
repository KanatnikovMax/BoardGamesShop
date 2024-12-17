using System.Text;
using AutoMapper;
using BoardGamesShop.BusinessLogic.Authorization;
using BoardGamesShop.BusinessLogic.Authorization.Entities;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.BusinessLogic.Users.Exceptions;
using BoardGamesShopWebApp.Controllers.Authorization.Entities;
using BoardGamesShopWebApp.Controllers.Users.Entities;
using BoardGamesShopWebApp.Validator.Users;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace BoardGamesShopWebApp.Controllers.Authorization;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthorizationProvider _authorizationProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public AuthorizationController(IAuthorizationProvider authorizationProvider, IMapper mapper, ILogger logger)
    {
        _authorizationProvider = authorizationProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromQuery] RegisterUserRequest request)
    {
        try
        {
            var validationResult = await new RegisterUserRequestValidator().ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                var stringBuilder = new StringBuilder();
                foreach (var error in errors)
                    stringBuilder.AppendLine(error);
                _logger.Error(stringBuilder.ToString());
                return BadRequest(errors);
            }
        
            var registerModel = _mapper.Map<RegisterUserModel>(request);
            var userModel = await _authorizationProvider.RegisterUserAsync(registerModel);
            return Ok(new UsersListResponse
            {
                Users = [userModel]
            });
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromQuery] AuthorizeUserRequest request)
    {
        try
        {
            var authorizeModel = _mapper.Map<AuthorizeUserModel>(request);
            var tokens = await _authorizationProvider.LoginUserAsync(authorizeModel);

            return Ok(tokens);
        }
        catch (BusinessLogicException e) when (e.ResultCode == ResultCode.UserNotFound)
        {
            _logger.Error(e.Message);
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error(e.Message);
            return BadRequest(e.Message);
        }
    }
}