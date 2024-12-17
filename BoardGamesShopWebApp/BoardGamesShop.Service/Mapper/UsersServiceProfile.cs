using AutoMapper;
using BoardGamesShop.BusinessLogic.Authorization.Entities;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShopWebApp.Controllers.Authorization.Entities;
using BoardGamesShopWebApp.Controllers.Users.Entities;

namespace BoardGamesShopWebApp.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserModel>()
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        CreateMap<UpdateUserRequest, UpdateUserModel>();
        CreateMap<UserFilter, UserModelFilter>();
        CreateMap<AuthorizeUserRequest, AuthorizeUserModel>();
    }
}