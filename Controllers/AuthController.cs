using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using modLib.BL;
using modLib.Entities.DTO.Auth;

namespace modLib.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IValidator<RegisterModel> _registerValidator;
        private readonly AuthService _authService;

        public AuthController(IValidator<RegisterModel> registerValidator, AuthService authService)
        {
            _registerValidator = registerValidator;
            _authService = authService;
        }

        [HttpPost("/auth/register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var validationResult = _registerValidator.Validate(registerModel);           
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            try
            {
                await _authService.Register(registerModel);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while register new user.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
