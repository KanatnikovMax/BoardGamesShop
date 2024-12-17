using BoardGamesShop.DataAccess;
using BoardGamesShopWebApp.Settings;
using Microsoft.EntityFrameworkCore;

namespace BoardGamesShopWebApp.IoC;

public static class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services, BoardGamesShopSettings settings)
    {
        services.AddDbContextFactory<BoardGamesShopDbContext>(options =>
        {
            options.UseNpgsql(settings.BoardGamesShopDbConnectionString);
        }, ServiceLifetime.Scoped);

    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<BoardGamesShopDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}