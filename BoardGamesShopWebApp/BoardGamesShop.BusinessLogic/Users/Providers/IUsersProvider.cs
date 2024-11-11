using BoardGamesShop.BusinessLogic.Users.Entities;

namespace BoardGamesShop.BusinessLogic.Users.Managers;

public interface IUsersProvider
{
    IEnumerable<UserModel> GetAllUsers(UserModelFilter filter = null);
    UserModel GetUserInfo(int userId);
}