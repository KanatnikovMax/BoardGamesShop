using BoardGamesShopWebApp.Controllers.Authorization;
using BoardGamesShopWebApp.IoC;
using BoardGamesShopWebApp.Settings;

namespace BoardGamesShopWebApp.DI;

public class AppConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, BoardGamesShopSettings settings)
    {
        AuthorizationConfigurator.ConfigureServices(builder.Services, settings);
        DbContextConfigurator.ConfigureService(builder.Services, settings);
        SerilogConfigurator.ConfigureService(builder);
        SwaggerConfigurator.ConfigureServices(builder.Services);
        MapperConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services, settings);

        builder.Services.AddControllers();
    }

    public static async Task ConfigureApplication(WebApplication app, BoardGamesShopSettings settings)
    {
        AuthorizationConfigurator.ConfigureApplication(app);
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
        var repositoryInitializer = new RepositoryInitializer(settings);
        await repositoryInitializer.InitializeRepository(app);
        app.MapControllers();
        //app.UseHttpsRedirection();
    }
}