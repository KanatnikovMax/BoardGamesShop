using AutoMapper;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.DataAccess.Entities;

namespace BoardGamesShop.BusinessLogic.Mapper;

public class UsersBLProfile : Profile
{
    public UsersBLProfile()
    {
        CreateMap<UserEntity, UserModel>()
            .ForMember(dest => dest.FullName, 
                opt => opt.MapFrom(src => $"{src.LastName} {src.FirstName} {src.Patronymic}"));
        
        CreateMap<CreateUserModel, UserEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.Ignore())
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.ModificationTime, opt => opt.Ignore());
        
        CreateMap<UpdateUserModel, UserEntity>()
            .ForMember(dest => dest.CreationTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FullName.Split()[0]))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FullName.Split()[1]))
            .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.FullName.Split()[2]));
    }
}