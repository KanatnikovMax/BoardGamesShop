using AutoMapper;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;

namespace BoardGamesShop.BusinessLogic.Users.Managers;

public class UserProvider : IUsersProvider
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IMapper _mapper;
    
    public UserProvider(IRepository<UserEntity> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public IEnumerable<UserModel> GetAllUsers(UserModelFilter filter = null)
    {
        var loginPart = filter?.LoginPart;
        var emailPart = filter?.EmailPart;
        var cityPart = filter?.CityPart;
        var phoneNumberPart = filter?.PhoneNumberPart;
        var role = filter?.Role;
        var creationTime = filter?.CreationTime;
        
        var users = _userRepository.GetAll(u =>
            (loginPart == null || u.Login.Contains(loginPart)) &&
            (emailPart == null || u.Email.Contains(emailPart)) &&
            (cityPart == null || u.City.Contains(cityPart)) &&
            (phoneNumberPart == null || u.PhoneNumber.Contains(phoneNumberPart)) &&
            (role == null || u.Role == role) &&
            (creationTime == null || u.CreationTime == creationTime));
        
        return _mapper.Map<IEnumerable<UserModel>>(users);
    }

    public UserModel GetUserInfo(int userId)
    {
        var user = _userRepository.GetById(userId);
        if (user is null)
        {
            throw new ArgumentException("User not found");
        }
        
        return _mapper.Map<UserModel>(user);
    }
}