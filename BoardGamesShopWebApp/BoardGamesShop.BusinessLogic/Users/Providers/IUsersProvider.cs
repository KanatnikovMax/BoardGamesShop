using BoardGamesShop.BusinessLogic.Users.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Managers;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetAllUsers(UserModelFilter filter = null);
    Task<IEnumerable<UserModel>> GetAllUsersAsync(UserModelFilter filter = null);
    UserModel GetUserInfo(int userId);
    Task<UserModel> GetUserInfoAsync(int userId);
}