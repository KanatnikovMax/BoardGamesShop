using BoardGamesShop.BusinessLogic.BoardGames.Entities;

namespace BoardGamesShop.BusinessLogic.BoardGames.Managers;

public interface IBoardGamesManager
{
    BoardGameModel CreateBoardGame(CreateBoardGameModel model);
    Task<BoardGameModel> CreateUBoardGameAsync(CreateBoardGameModel model);
    void DeleteBoardGame(int userId);
    Task DeleteBoardGameAsync(int userId);
    /*BoardGameModel UpdateBoardGame(UpdateBoardGameModel model, int userId);
    Task<BoardGameModel> UpdateBoardGameAsync(UpdateBoardGameModel model, int userId);*/
}