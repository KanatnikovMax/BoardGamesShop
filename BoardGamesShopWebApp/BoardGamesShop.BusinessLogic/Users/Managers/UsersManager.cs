using AutoMapper;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.BusinessLogic.Users.Exceptions;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;

namespace BoardGamesShop.BusinessLogic.Users.Managers;

public class UsersManager : IUsersManager
{
    private readonly IRepository<UserEntity> _usersRepository;
    private readonly IMapper _mapper;

    public UsersManager(IRepository<UserEntity> usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public UserModel CreateUser(CreateUserModel model)
    {
       var entity = _mapper.Map<UserEntity>(model);
       entity = _usersRepository.Save(entity);
       return _mapper.Map<UserModel>(entity);
    }

    public void DeleteUser(int userId)
    {
        var entity = _usersRepository.GetById(userId);
        if (entity == null)
        {
            throw new UserNotFoundException("User not found");
        }
        _usersRepository.Delete(entity);
    }


    public UserModel UpdateUser(UpdateUserModel model, int userId)
    {
        var entity = _usersRepository.GetById(userId);
        if (entity == null)
        {
            throw new UserNotFoundException("User not found");
        }
        entity = _mapper.Map<UserEntity>(model);
        entity = _usersRepository.Save(entity);
        return _mapper.Map<UserModel>(entity);
    }
}