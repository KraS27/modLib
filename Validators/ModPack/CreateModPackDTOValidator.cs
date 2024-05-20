using FluentValidation;
using modLib.Entities.DTO.ModPacks;

namespace modLib.Validators.ModPack
{
    public class CreateModPackDTOValidator : AbstractValidator<CreateModPackDTO>
    {
        public CreateModPackDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 60).WithMessage("Name must be between 2 and 60 characters");

            RuleFor(x => x.GameId)
                .GreaterThan(0).WithMessage("GameId must be greater than 0");
        }
    }
}
