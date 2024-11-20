using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Entities;

public class CreateUserModel
{
    public string Login { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; } 
    public string? Patronymic { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}