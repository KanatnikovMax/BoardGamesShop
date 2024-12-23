﻿using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Entities;

public class UserModel
{
    public int Id { get; set; }
    public Guid ExternalId { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; } 
    public string City { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
}