using System.Net;
using BoardGamesShop.BusinessLogic.Authorization;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.Service.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using BoardGamesShop.BusinessLogic.Authorization.Entities;
using BoardGamesShop.BusinessLogic.Users.Managers;
using BoardGamesShop.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;

namespace BoardGamesShop.Service.Tests.Authorization;

public class AuthorizeUserTests : TestBase
{
    [Test]
    public async Task LoginUser_Successfully()
    {
        var password = "p@ssw0rddd";
        var registerUserModel = new RegisterUserModel
        {
            Email = "user@email.com",
            UserName = "user@email.com",
            Password = password,
            FirstName = "user",
            LastName = "user",
            Patronymic = "user",
            City = "city", 
            PhoneNumber = "89734831886"
        };
        
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthorizationProvider>();
        
        var userManagerIdentity = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var user = await userManagerIdentity.FindByEmailAsync(registerUserModel.Email);
        
        UserModel userModel = user is null
            ? await authProvider.RegisterUserAsync(registerUserModel)
            : MapperHelper.Mapper.Map<UserModel>(user);

        var query = $"?email={userModel.Email}&password={password}";
        var requestUri = BoardGamesShopEndpoints.LoginEndpoint + query;
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var responseContentJson = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<TokenResponse>(responseContentJson);
        
        content.Should().NotBeNull();
        content.AccessToken.Should().NotBeNull();
        content.RefreshToken.Should().NotBeNull();
        
        var userManager = scope.ServiceProvider.GetRequiredService<IUsersManager>();
        await userManager.DeleteUserAsync(userModel.Id);
    }
    
    [Test]
    public async Task LoginUser_WrongPassword()
    {
        var password = "p@ssw0rddd";
        var registerUserModel = new RegisterUserModel
        {
            Email = "user@email.com",
            UserName = "user@email.com",
            Password = password,
            FirstName = "user",
            LastName = "user",
            Patronymic = "user",
            City = "city", 
            PhoneNumber = "89734831886"
        };
        
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        
        var authProvider = scope.ServiceProvider.GetRequiredService<IAuthorizationProvider>();
        
        var userManagerIdentity = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var user = await userManagerIdentity.FindByEmailAsync(registerUserModel.Email);
        
        UserModel userModel = user is null
            ? await authProvider.RegisterUserAsync(registerUserModel)
            : MapperHelper.Mapper.Map<UserModel>(user);

        var wrongPassword = "cfvghnefr234@v";
        
        var query = $"?email={userModel.Email}&password={wrongPassword}";
        var requestUri = BoardGamesShopEndpoints.LoginEndpoint + query;
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var userManager = scope.ServiceProvider.GetRequiredService<IUsersManager>();
        await userManager.DeleteUserAsync(userModel.Id);
    }
    
    [Test]
    public async Task LoginUser_UserNotFound()
    {
        var password = "p@ssw0rddd";
        var email = "user@email.com";
        
        using var scope = GetService<IServiceScopeFactory>().CreateScope();
        
        var userManagerIdentity = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
        var user = await userManagerIdentity.FindByEmailAsync(email);
        var userManager = scope.ServiceProvider.GetRequiredService<IUsersManager>();
        
        if (user is not null)
        {
            await userManager.DeleteUserAsync(user.Id);
        }
        
        var query = $"?email={email}&password={password}";
        var requestUri = BoardGamesShopEndpoints.LoginEndpoint + query;
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var client = TestHttpClient;
        var response = await client.SendAsync(request);
        
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}