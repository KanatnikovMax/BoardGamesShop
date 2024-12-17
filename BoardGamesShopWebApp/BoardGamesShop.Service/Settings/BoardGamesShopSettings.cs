namespace BoardGamesShopWebApp.Settings;

public class BoardGamesShopSettings
{
    public string BoardGamesShopDbConnectionString { get; set; }
    public string IdentityServerUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string MasterAdminEmail { get; set; }
    public string MasterAdminPassword { get; set; }
}