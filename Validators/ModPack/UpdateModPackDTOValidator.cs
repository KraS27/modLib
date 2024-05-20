using FluentValidation;
using modLib.Entities.DTO.ModPacks;

namespace modLib.Validators.ModPack
{
    public class UpdateModPackDTOValidator : AbstractValidator<UpdateModPackDTO>
    {
        public UpdateModPackDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 60).WithMessage("Name must be between 2 and 60 characters");          
        }
    }
}
