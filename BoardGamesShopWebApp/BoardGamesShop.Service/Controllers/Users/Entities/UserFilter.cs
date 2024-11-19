using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShopWebApp.Controllers.Users.Entities;

public class UserFilter
{
    public string? LoginPart { get; set; }
    public string? EmailPart { get; set; }
    public string? CityPart { get; set; }
    public string? PhoneNumberPart { get; set; }
    public Role? Role { get; set; }
    public DateTime? CreationTime { get; set; }
}