using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using modLib.BL;
using modLib.Entities;
using modLib.Entities.DTO.Game;
using modLib.Entities.DTO.Mods;
using modLib.Entities.Exceptions;
using modLib.Validators.Mod;

namespace modLib.Controllers
{
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly GameService _service;
        private readonly IValidator<CreateGameDTO> _createGameValidator;
        private readonly IValidator<UpdateGameDTO> _updateGameValidator;

        public GameController(ILogger<GameController> logger, GameService service, IValidator<CreateGameDTO> createGameValidator, IValidator<UpdateGameDTO> updateGameValidator)
        {
            _logger = logger;
            _service = service;
            _createGameValidator = createGameValidator;
            _updateGameValidator = updateGameValidator;
        }

        [HttpGet("/games/{id}")]
        public async Task<IActionResult> GetGame(int id)
        {
            try
            {
                var game = await _service.GetAsync(id);

                return game == null ? NoContent() : Ok(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting game.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet("/games")]
        public async Task<IActionResult> GetGames([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            try
            {
                var games = await _service.GetAllAsync(new Pagination<GetGamesDTO>(page, pageSize));

                return Ok(games);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while getting game.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPost("/games")]
        public async Task<IActionResult> AddGame([FromBody] CreateGameDTO createModel)
        {
            var validationResult = await _createGameValidator.ValidateAsync(createModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

            try
            {
                await _service.CreateAsync(createModel);

                return Ok();
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }           
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding game.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpDelete("games/{id}")]
        public async Task<IActionResult> RemoveMod(int id)
        {
            try
            {
                await _service.RemoveAsync(id);

                return Ok();
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while removing game.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpPut("games")]
        public async Task<IActionResult> UpdateMod([FromBody] UpdateGameDTO updateModel)
        {
            try
            {
                var validationResult = await _updateGameValidator.ValidateAsync(updateModel);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

                await _service.UpdateAsync(updateModel);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating game.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred. Please try again later." });
            }
        }
    }
}
