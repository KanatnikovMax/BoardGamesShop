using AutoMapper;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShopWebApp.Controllers.Users.Entities;

namespace BoardGamesShopWebApp.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<RegisterUserRequest, CreateUserModel>();
        CreateMap<UpdateUserRequest, UpdateUserModel>();
        CreateMap<UserFilter, UserModelFilter>();
    }
}