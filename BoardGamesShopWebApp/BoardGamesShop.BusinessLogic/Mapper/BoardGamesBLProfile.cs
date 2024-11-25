using AutoMapper;
using BoardGamesShop.BusinessLogic.BoardGames.Entities;
using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShop.BusinessLogic.Mapper;

public class BoardGamesBLProfile : Profile
{
    public BoardGamesBLProfile()
    {
        CreateMap<BoardGame, BoardGameModel>();
        
        CreateMap<CreateBoardGameModel, BoardGame>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.ModificationTime, opt => opt.Ignore());
    }
}