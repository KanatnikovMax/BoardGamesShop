using BoardGamesShop.BusinessLogic.Authorization.Entities;
using BoardGamesShop.BusinessLogic.Users.Entities;

namespace BoardGamesShop.BusinessLogic.Authorization;

public interface IAuthorizationProvider
{
    Task<UserModel> RegisterUserAsync(RegisterUserModel model);
    Task<TokenResponse> LoginUserAsync(AuthorizeUserModel model);
}