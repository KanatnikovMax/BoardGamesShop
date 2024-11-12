﻿using BoardGamesShop.BusinessLogic.Users.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Managers;

public interface IUsersManager
{
    UserModel CreateUser(CreateUserModel model);
    // TODO: добавить изменение
    void DeleteUser(int userId);
    //UserModel UpdateUser(UpdateUserModel user);

}