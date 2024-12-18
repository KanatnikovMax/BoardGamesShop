using BoardGamesShop.BusinessLogic.BoardGames.Entities;

namespace BoardGamesShop.BusinessLogic.BoardGames.Managers;

public interface IBoardGamesManager
{
    BoardGameModel CreateBoardGame(CreateBoardGameModel model);
    Task<BoardGameModel> CreateBoardGameAsync(CreateBoardGameModel model);
    void DeleteBoardGame(int id);
    Task DeleteBoardGameAsync(int id);
    BoardGameModel UpdateBoardGame(UpdateBoardGameModel model, int id);
    Task<BoardGameModel> UpdateBoardGameAsync(UpdateBoardGameModel model, int id);
}