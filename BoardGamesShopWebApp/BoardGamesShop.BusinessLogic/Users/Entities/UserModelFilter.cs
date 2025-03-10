﻿using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Entities;

public class UserModelFilter
{
    public string? UserNamePart { get; set; }
    public string? EmailPart { get; set; }
    public string? CityPart { get; set; }
    public string? PhoneNumberPart { get; set; }
    public string Role { get; set; }
}