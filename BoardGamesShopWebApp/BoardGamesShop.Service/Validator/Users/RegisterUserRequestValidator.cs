﻿using BoardGamesShopWebApp.Controllers.Users.Entities;
using FluentValidation;

namespace BoardGamesShopWebApp.Validator.Users;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email address is required")
            .MaximumLength(255)
            .WithMessage("Email must be less than 255 characters");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required")
            .Matches(@"^8\d{10}$")
            .WithMessage("Phone number is invalid");

        RuleFor(x => x.Login)
            .NotEmpty()
            .WithMessage("Login is required")
            .MaximumLength(50)
            .WithMessage("Login must be less than 50 characters");
        
        RuleFor(x => x.PasswordHash)
            .NotEmpty()
            .WithMessage("Password is required")
            .MaximumLength(50)
            .WithMessage("Password must be less than 255 characters");
        
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City is required")
            .MaximumLength(50)
            .WithMessage("City must be less than 50 characters")
            .Matches(@"\w|-|\s")
            .WithMessage("City is invalid");
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required")
            .MaximumLength(50)
            .WithMessage("Last name must be less than 50 characters")
            .Matches(@"[\w|-]+")
            .WithMessage("Last name is invalid");
        
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required")
            .MaximumLength(50)
            .WithMessage("First name must be less than 50 characters")
            .Matches(@"[\w|-]+")
            .WithMessage("First name is invalid");
        
        RuleFor(x => x.Patronymic)
            .MaximumLength(50)
            .WithMessage("Patronymic must be less than 50 characters")
            .Matches("w+")
            .WithMessage("Patronymic is invalid");
    }
}