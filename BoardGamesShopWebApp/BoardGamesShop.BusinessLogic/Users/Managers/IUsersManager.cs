using BoardGamesShop.BusinessLogic.Users.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Managers;

public interface IUsersManager
{
    UserModel CreateUser(CreateUserModel model);
    void DeleteUser(int userId);
    UserModel UpdateUser(UpdateUserModel model, int userId);

}