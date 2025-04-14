using FluentValidation;
using SB.StateHub.API.DTOs.Users;

namespace SB.StateHub.API.FluentValidation.Validators.Users
{
    public class UserValidator : AbstractValidator<CreateOrUpdateUserDto>
    {
        public UserValidator()
        {
            RuleFor(get => get.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(get => get.Lastname).NotEmpty().WithMessage("Lastname is required");
            RuleFor(get => get.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(get => get.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}