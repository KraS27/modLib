using FluentValidation;
using modLib.Entities.DTO.Mods;

namespace modLib.Validators.Mod
{
    public class CreateModDTOValidator : AbstractValidator<CreateModDTO>
    {
        public CreateModDTOValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 60).WithMessage("Name must be between 2 and 60 characters");

            RuleFor(x => x.Path)
                .Length(2, 60).WithMessage("Path must be between 2 and 120 characters");

            RuleFor(x => x.GameId)
                .GreaterThan(0).WithMessage("GameId must be greater than 0");
        }
    }
}
