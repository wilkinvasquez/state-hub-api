using FluentValidation;
using SB.StateHub.API.DTOs.GovermentEntities;

namespace SB.StateHub.API.FluentValidation.Validators.GovermentEntities
{
    public class GovermentEntityValidator : AbstractValidator<CreateOrUpdateGovermentEntityDto>
    {
        public GovermentEntityValidator()
        {
            RuleFor(gen => gen.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(gen => gen.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}