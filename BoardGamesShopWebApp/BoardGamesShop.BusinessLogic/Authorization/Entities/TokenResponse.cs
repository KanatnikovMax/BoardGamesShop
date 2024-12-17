namespace BoardGamesShop.BusinessLogic.Authorization.Entities;

public class TokenResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}