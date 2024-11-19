namespace BoardGamesShopWebApp.Controllers.Users.Entities;

public class UpdateUserRequest
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public string Login { get; set; }
    public string FullName { get; set; } 
    public string City { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime ModificationTime { get; set; }
}