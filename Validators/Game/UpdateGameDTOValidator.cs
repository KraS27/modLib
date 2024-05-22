using FluentValidation;
using modLib.Entities.DTO.Game;

namespace modLib.Validators.Game
{
    public class UpdateGameDTOValidator : AbstractValidator<UpdateGameDTO>
    {
        public UpdateGameDTOValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Id is required")
               .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 60).WithMessage("Name must be between 2 and 64 characters");
        }
    }
}
