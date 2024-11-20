using AutoMapper;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.BusinessLogic.Users.Exceptions;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;

namespace BoardGamesShop.BusinessLogic.Users.Managers;

public class UsersProvider : IUsersProvider
{
    private readonly IRepository<UserEntity> _usersRepository;
    private readonly IMapper _mapper;
    
    public UsersProvider(IRepository<UserEntity> usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public IEnumerable<UserModel> GetAllUsers(UserModelFilter filter = null)
    {
        var loginPart = filter?.LoginPart;
        var emailPart = filter?.EmailPart;
        var cityPart = filter?.CityPart;
        var phoneNumberPart = filter?.PhoneNumberPart;
        var role = filter?.Role;
        
        var users = _usersRepository.GetAll(u =>
            (loginPart == null || u.Login.Contains(loginPart)) &&
            (emailPart == null || u.Email.Contains(emailPart)) &&
            (cityPart == null || u.City.Contains(cityPart)) &&
            (phoneNumberPart == null || u.PhoneNumber.Contains(phoneNumberPart)) &&
            (role == null || u.Role == role));
        
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public UserModel GetUserInfo(int userId)
    {
        var user = _usersRepository.GetById(userId);
        if (user is null)
        {
            throw new UserNotFoundException("User not found");
        }
        
        return _mapper.Map<UserModel>(user);
    }
}