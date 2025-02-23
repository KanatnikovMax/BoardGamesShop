using BoardGamesShopWebApp.Settings;

namespace BoardGamesShop.Service.Tests.Helpers;

public static class TestSettingsHelper
{
    public static BoardGamesShopSettings GetBoardGamesShopSettings()
    {
        return BoardGamesShopSettingsReader.Read(ConfigurationHelper.GetConfiguration());
    }
}