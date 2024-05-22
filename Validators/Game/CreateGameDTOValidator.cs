using FluentValidation;
using modLib.Entities.DTO.Game;

namespace modLib.Validators.Game
{
    public class CreateGameDTOValidator : AbstractValidator<CreateGameDTO>
    {
        public CreateGameDTOValidator()
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 60).WithMessage("Name must be between 2 and 64 characters");            
        }
    }
}
