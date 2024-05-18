using FluentValidation;
using modLib.Entities.DTO.Mods;

namespace modLib.Validators.Mod
{
    public class UpdateModDTOValidator : AbstractValidator<UpdateModDTO>
    {
        public UpdateModDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Name is required.")
               .Length(2, 60).WithMessage("Name must be between 2 and 60 characters");

            RuleFor(x => x.Path)
                .Length(2, 60).WithMessage("Path must be between 2 and 120 characters");          
        }
    }
}
