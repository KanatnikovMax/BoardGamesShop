using AutoMapper;
using BoardGamesShop.BusinessLogic.BoardGames.Entities;
using BoardGamesShopWebApp.Controllers.BoardGames.Entities;

namespace BoardGamesShopWebApp.Mapper;

public class BoardGamesServiceProfile : Profile
{
    public BoardGamesServiceProfile()
    {
        CreateMap<CreateBoardGameRequest, CreateBoardGameModel>();
        CreateMap<BoardGameFilter, BoardGameModelFilter>();
        CreateMap<UpdateBoardGameRequest, UpdateBoardGameModel>();
    }
}