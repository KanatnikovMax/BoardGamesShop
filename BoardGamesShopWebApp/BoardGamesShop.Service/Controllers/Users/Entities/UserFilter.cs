using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShopWebApp.Controllers.Users.Entities;

public class UserFilter
{
    public string? UserNamePart { get; set; }
    public string? EmailPart { get; set; }
    public string? CityPart { get; set; }
    public string? PhoneNumberPart { get; set; }
    public string? Role { get; set; }
}