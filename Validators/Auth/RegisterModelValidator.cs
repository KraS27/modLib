using FluentValidation;
using modLib.Entities.DTO.Auth;

namespace modLib.Validators.Auth
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username required")
                .Length(2, 12).WithMessage("Username must be between 2 and 12 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password required")
                .Length(8, 32).WithMessage("Password must be between 8 and 32 characters");                
        }
    }
}
