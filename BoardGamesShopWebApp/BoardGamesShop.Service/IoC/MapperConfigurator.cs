using BoardGamesShop.BusinessLogic.Mapper;
using BoardGamesShopWebApp.Mapper;

namespace BoardGamesShopWebApp.IoC;

public static class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            // users
            config.AddProfile<UsersBLProfile>();
            config.AddProfile<UsersServiceProfile>();
            // board games
            config.AddProfile<BoardGamesBLProfile>();
            config.AddProfile<BoardGamesServiceProfile>();
        });
    }
}