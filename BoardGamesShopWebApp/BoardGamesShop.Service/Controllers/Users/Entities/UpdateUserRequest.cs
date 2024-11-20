namespace BoardGamesShopWebApp.Controllers.Users.Entities;

public class UpdateUserRequest
{
    public string? Login { get; set; }
    public string? LastName { get; set; }
    public string? FirstName { get; set; } 
    public string? Patronymic { get; set; }
    public string? City { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string? PhoneNumber { get; set; }
}