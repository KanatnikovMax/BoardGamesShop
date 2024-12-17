using BoardGamesShop.BusinessLogic.BoardGames.Entities;
using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShopWebApp.Controllers.BoardGames.Entities;

public class BoardGamesListResponse
{
    public List<BoardGameModel> BoardGames { get; set; }
}