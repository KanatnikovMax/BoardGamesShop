using AutoMapper;
using BoardGamesShop.BusinessLogic.Mapper;

namespace BoardGamesShop.BusinessLogic.UnitTests.Mapper;

public static class MapperHelper
{
    static MapperHelper()
    {
        var config = new MapperConfiguration(x => x.AddProfile(typeof(BoardGamesBLProfile)));
        Mapper = new AutoMapper.Mapper(config);
    }
    
    public static IMapper Mapper { get; }
}