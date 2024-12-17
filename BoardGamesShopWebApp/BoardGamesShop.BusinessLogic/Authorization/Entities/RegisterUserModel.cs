﻿using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Entities;

public class RegisterUserModel
{
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; } 
    public string? Patronymic { get; set; }
    public string City { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; } = Role.Customer;
}