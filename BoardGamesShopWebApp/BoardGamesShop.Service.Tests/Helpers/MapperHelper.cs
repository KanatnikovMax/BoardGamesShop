using AutoMapper;
using BoardGamesShop.BusinessLogic.Mapper;

namespace BoardGamesShop.Service.Tests.Helpers;

public static class MapperHelper
{
    static MapperHelper()
    {
        var config = new MapperConfiguration(x => x.AddProfile(typeof(UsersBLProfile)));
        Mapper = new AutoMapper.Mapper(config);
    }
    
    public static IMapper Mapper { get; }
}