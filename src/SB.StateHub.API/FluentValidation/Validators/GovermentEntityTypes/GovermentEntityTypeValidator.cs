using FluentValidation;
using SB.StateHub.API.DTOs.GovermentEntityTypes;

namespace SB.StateHub.API.FluentValidation.Validators.GovermentEntityTypes
{
    public class GovermentEntityTypeValidator : AbstractValidator<CreateOrUpdateGovermentEntityTypeDto>
    {
        public GovermentEntityTypeValidator()
        {
            RuleFor(get => get.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}