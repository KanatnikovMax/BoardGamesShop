using AutoMapper;
using BoardGamesShop.BusinessLogic.Users.Entities;
using BoardGamesShop.BusinessLogic.Users.Exceptions;
using BoardGamesShop.DataAccess.Entities;
using BoardGamesShop.DataAccess.Repository;
using Duende.IdentityServer.Models;

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

    public void DeleteUser(int userId)
    {
        var entity = _usersRepository.GetById(userId);
        if (entity == null)
        {
            throw new BusinessLogicException(ResultCode.UserNotFound);
        }
        _usersRepository.Delete(entity);
    }

    public async Task DeleteUserAsync(int userId)
    {
        var entity = await _usersRepository.GetByIdAsync(userId);
        if (entity == null)
        {
            throw new BusinessLogicException(ResultCode.UserNotFound);
        }
        await _usersRepository.DeleteAsync(entity);
    }


    public UserModel UpdateUser(UpdateUserModel model, int userId)
    {
        var entity = _usersRepository.GetById(userId);
        if (entity == null)
        {
            throw new BusinessLogicException(ResultCode.UserNotFound);
        }

        entity = _mapper.Map<UpdateUserModel, UserEntity>(model, opts => opts.AfterMap(
            (src, dest) =>
            {
                dest.Id = userId;
                dest.ExternalId = entity.ExternalId;
                dest.CreationTime = entity.CreationTime;
                dest.Role = entity.Role;
                dest.UserName = src.UserName is null ? entity.UserName : dest.UserName;
                dest.City = src.City is null ? entity.City : dest.City;
                dest.PhoneNumber = src.PhoneNumber is null ? entity.PhoneNumber : dest.PhoneNumber;
                dest.Email = src.Email is null ? entity.Email : dest.Email;
                dest.LastName = src.LastName is null ? entity.LastName : dest.LastName;
                dest.FirstName = src.FirstName is null ? entity.FirstName : dest.FirstName;
                dest.Patronymic = src.Patronymic is null ? entity.Patronymic : dest.Patronymic;
            }));
        try
        {
            entity = _usersRepository.Save(entity);
            return _mapper.Map<UserModel>(entity);
        }
        catch (Exception e)
        {
            throw new BusinessLogicException(ResultCode.UserAlreadyExists);
        }
    }

    public async Task<UserModel> UpdateUserAsync(UpdateUserModel model, int userId)
    {
        var entity = await _usersRepository.GetByIdAsync(userId);
        if (entity == null)
        {
            throw new BusinessLogicException(ResultCode.UserNotFound);
        }

        entity = _mapper.Map<UpdateUserModel, UserEntity>(model, opts => opts.AfterMap(
            (src, dest) =>
            {
                dest.Id = userId;
                dest.ExternalId = entity.ExternalId;
                dest.CreationTime = entity.CreationTime;
                dest.Role = entity.Role;
                dest.UserName = src.UserName is null ? entity.UserName : dest.UserName;
                dest.City = src.City is null ? entity.City : dest.City;
                dest.PhoneNumber = src.PhoneNumber is null ? entity.PhoneNumber : dest.PhoneNumber;
                dest.Email = src.Email is null ? entity.Email : dest.Email;
                dest.LastName = src.LastName is null ? entity.LastName : dest.LastName;
                dest.FirstName = src.FirstName is null ? entity.FirstName : dest.FirstName;
                dest.Patronymic = src.Patronymic is null ? entity.Patronymic : dest.Patronymic;
            }));
        try
        {
            entity = await _usersRepository.SaveAsync(entity);
            return _mapper.Map<UserModel>(entity);
        }
        catch (Exception e)
        {
            throw new BusinessLogicException(ResultCode.UserAlreadyExists);
        }
    }
}