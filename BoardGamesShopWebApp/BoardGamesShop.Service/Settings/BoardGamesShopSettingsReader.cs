namespace BoardGamesShopWebApp.Settings;

public class BoardGamesShopSettingsReader
{
    public static BoardGamesShopSettings Read(IConfiguration configuration)
    {
        return new BoardGamesShopSettings()
        {
            BoardGamesShopDbConnectionString = configuration.GetValue<string>("BoardGamesShopDbContext"),
            IdentityServerUri = configuration.GetValue<string>("IdentityServerSettings:Uri"),
            ClientId = configuration.GetValue<string>("IdentityServerSettings:ClientId"),
            ClientSecret = configuration.GetValue<string>("IdentityServerSettings:ClientSecret")
        };
    }
}