using BoardGamesShop.BusinessLogic.Mapper;

namespace BoardGamesShopWebApp.IoC;

public static class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UsersBLProfile>();
        });
    }
}