using BoardGamesShop.BusinessLogic.Mapper;
using BoardGamesShopWebApp.Mapper;

namespace BoardGamesShopWebApp.IoC;

public static class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UsersBLProfile>();
            config.AddProfile<UsersServiceProfile>();
        });
    }
}