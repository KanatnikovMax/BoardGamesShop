using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BoardGamesShop.DataAccess.Entities;

[Table("users")]
public class UserEntity : IdentityUser<int>, IEntity
{
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    public string? FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? City { get; set; }
    public UserRole Role { get; set; }
    
    public List<PurchaseHistory> PurchaseHistory { get; set; }
    
    public List<GameDay> GameDays { get; set; }
}

public class UserRole : IdentityRole<int>
{
    public UserRole()
    {
    }
    
    public UserRole(string roleName) : base(roleName)
    {
    }
}