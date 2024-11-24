using BoardGamesShop.BusinessLogic.Users.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Managers;

public interface IUsersManager
{
    UserModel CreateUser(CreateUserModel model);
    Task<UserModel> CreateUserAsync(CreateUserModel model);
    void DeleteUser(int userId);
    Task DeleteUserAsync(int userId);
    UserModel UpdateUser(UpdateUserModel model, int userId);
    Task<UserModel> UpdateUserAsync(UpdateUserModel model, int userId);

}