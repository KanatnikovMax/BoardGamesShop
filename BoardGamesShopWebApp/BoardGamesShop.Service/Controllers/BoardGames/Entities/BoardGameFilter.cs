﻿namespace BoardGamesShopWebApp.Controllers.BoardGames.Entities;

public class BoardGameFilter
{
    public string? NamePart { get; set; }
    public string? GenrePart { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
    public int? MinAge { get; set; }
    public string? PublisherPart { get; set; }
}