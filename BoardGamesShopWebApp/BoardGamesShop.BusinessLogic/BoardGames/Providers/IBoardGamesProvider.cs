using BoardGamesShop.BusinessLogic.BoardGames.Entities;

namespace BoardGamesShop.BusinessLogic.BoardGames.Providers;

public interface IBoardGamesProvider
{
    IEnumerable<BoardGameModel> GetAllBoardGames(BoardGameModelFilter filter = null);
    Task<IEnumerable<BoardGameModel>> GetAllBoardGamesAsync(BoardGameModelFilter filter = null);
    BoardGameModel GetBoardGameInfo(int userId);
    Task<BoardGameModel> GetBoardGameInfoAsync(int userId);
}