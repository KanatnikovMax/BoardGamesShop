using BoardGamesShopWebApp.Controllers.BoardGames.Entities;
using FluentValidation;

namespace BoardGamesShopWebApp.Validator.BoardGames;

public class UpdateBoardGameRequestValidator : AbstractValidator<UpdateBoardGameRequest>
{
    public UpdateBoardGameRequestValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}