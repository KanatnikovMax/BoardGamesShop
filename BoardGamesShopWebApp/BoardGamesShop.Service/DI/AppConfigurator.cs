using BoardGamesShopWebApp.IoC;
using BoardGamesShopWebApp.Settings;

namespace BoardGamesShopWebApp.DI;

public class AppConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, BoardGamesShopSettings settings)
    {
        DbContextConfigurator.ConfigureService(builder.Services, settings);
        SerilogConfigurator.ConfigureService(builder);
        SwaggerConfigurator.ConfigureServices(builder.Services);
        MapperConfigurator.ConfigureServices(builder.Services);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
        
        app.UseHttpsRedirection();
    }
}