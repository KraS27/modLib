using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using modLib.BL;
using modLib.Entities.DTO.Auth;
using modLib.Entities.Exceptions;

namespace modLib.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IValidator<RegisterModel> _registerValidator;
        private readonly IValidator<LoginModel> _loginValidator;
        private readonly AuthService _authService;

        public AuthController(IValidator<RegisterModel> registerValidator,
            AuthService authService,
            IValidator<LoginModel> loginValidator,
            ILogger<AuthController> logger)
        {
            _registerValidator = registerValidator;
            _authService = authService;
            _loginValidator = loginValidator;
            _logger = logger;
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
            catch(AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while register new user.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("/auth/login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var validationResult = _loginValidator.Validate(loginModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            try
            {
                var key = await _authService.Login(loginModel);
                HttpContext context = HttpContext;

                context.Response.Cookies.Append("jwt", key);

                return Ok(key);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidPasswordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while register new user.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
