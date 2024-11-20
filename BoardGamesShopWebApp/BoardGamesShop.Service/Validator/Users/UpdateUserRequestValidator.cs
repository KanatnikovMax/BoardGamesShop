using BoardGamesShopWebApp.Controllers.Users.Entities;
using FluentValidation;

namespace BoardGamesShopWebApp.Validator.Users;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
    }
}