using BoardGamesShop.BusinessLogic.Authorization;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShopWebApp.Settings;
using Microsoft.AspNetCore.Identity;

namespace BoardGamesShopWebApp.IoC;

public class RepositoryInitializer
{
    private readonly string _masterAdminEmail;
    private readonly string _masterAdminPassword;
    
    public RepositoryInitializer(BoardGamesShopSettings boardGamesShopSettings)
    {
        _masterAdminEmail = boardGamesShopSettings.MasterAdminEmail;
        _masterAdminPassword = boardGamesShopSettings.MasterAdminPassword;
    }
    
    private async Task CreateGlobalAdmin(IAuthorizationProvider authorizationProvider)
    {
        await authorizationProvider.RegisterUserAsync(new RegisterUserModel
        {
            Email = _masterAdminEmail,
            Password = _masterAdminPassword,
            Role = Role.Employee
        });
    }

    public async Task InitializeRepository(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var userManager = (UserManager<UserEntity>)scope.ServiceProvider
            .GetRequiredService(typeof(UserManager<UserEntity>));
        var user = await userManager.FindByEmailAsync(_masterAdminEmail);
        if (user == null)
        {
            var authorizationProvider = (IAuthorizationProvider)scope.ServiceProvider
                .GetRequiredService(typeof(IAuthorizationProvider));
            await CreateGlobalAdmin(authorizationProvider);
        }
    }
}