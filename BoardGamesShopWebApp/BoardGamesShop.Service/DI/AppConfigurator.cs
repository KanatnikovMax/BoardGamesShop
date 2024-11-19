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
        ServicesConfigurator.ConfigureServices(builder.Services, settings);

        builder.Services.AddControllers();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);

        app.MapControllers();
        app.UseHttpsRedirection();
    }
}