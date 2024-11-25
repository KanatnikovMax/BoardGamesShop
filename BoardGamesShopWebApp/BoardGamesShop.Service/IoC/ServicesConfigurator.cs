using AutoMapper;
using BoardGamesShop.BusinessLogic.BoardGames.Managers;
using BoardGamesShop.BusinessLogic.BoardGames.Providers;
using BoardGamesShop.BusinessLogic.Users.Managers;
using BoardGamesShop.DataAccess;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;
using BoardGamesShopWebApp.Settings;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShopWebApp.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, BoardGamesShopSettings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        // users
        services.AddScoped<IRepository<UserEntity>>(x =>
            new Repository<UserEntity>(x.GetRequiredService<IDbContextFactory<BoardGamesShopDbContext>>()));
        services.AddScoped<IUsersProvider>(x =>
            new UsersProvider(x.GetRequiredService<IRepository<UserEntity>>(),
            x.GetRequiredService<IMapper>()));
        services.AddScoped<IUsersManager>(x =>
            new UsersManager(x.GetRequiredService<IRepository<UserEntity>>(),
                x.GetRequiredService<IMapper>()));
        // board games
        services.AddScoped<IRepository<BoardGame>>(x =>
            new Repository<BoardGame>(x.GetRequiredService<IDbContextFactory<BoardGamesShopDbContext>>()));
        services.AddScoped<IBoardGamesProvider>(x =>
            new BoardGamesProvider(x.GetRequiredService<IRepository<BoardGame>>(),
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IBoardGamesManager>(x =>
            new BoardGamesManager(x.GetRequiredService<IRepository<BoardGame>>(),
                x.GetRequiredService<IMapper>()));
    }
}