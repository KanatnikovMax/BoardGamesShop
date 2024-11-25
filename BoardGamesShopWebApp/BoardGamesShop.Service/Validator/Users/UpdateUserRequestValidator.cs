using BoardGamesShopWebApp.Controllers.Users.Entities;
using FluentValidation;

namespace BoardGamesShopWebApp.Validator.Users;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email address is invalid")
            .MaximumLength(255)
            .WithMessage("Email must be less than 255 characters");
        
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^8\d{10}$")
            .WithMessage("Phone number is invalid");

        RuleFor(x => x.Login)
            .MaximumLength(50)
            .WithMessage("Login must be less than 50 characters");
        
        RuleFor(x => x.PasswordHash)
            .MaximumLength(50)
            .WithMessage("Password must be less than 255 characters");
        
        RuleFor(x => x.City)
            .MaximumLength(50)
            .WithMessage("City must be less than 50 characters")
            .Matches(@"^[a-zA-Zа-яА-ЯёЁ-\s]+$")
            .WithMessage("City is invalid");
        
        RuleFor(x => x.LastName)
            .MaximumLength(50)
            .WithMessage("Last name must be less than 50 characters")
            .Matches("^[a-zA-Zа-яА-ЯёЁ-]+$")
            .WithMessage("Last name is invalid");
        
        RuleFor(x => x.FirstName)
            .MaximumLength(50)
            .WithMessage("First name must be less than 50 characters")
            .Matches(@"^[a-zA-Zа-яА-ЯёЁ]+$")
            .WithMessage("First name is invalid");
        
        RuleFor(x => x.Patronymic)
            .MaximumLength(50)
            .WithMessage("Patronymic must be less than 50 characters")
            .Matches(@"^[a-zA-Zа-яА-ЯёЁ]+$")
            .WithMessage("Patronymic is invalid");
    }
}