using Microsoft.AspNetCore.Mvc;
using modLib.BL;
using modLib.Entities;
using modLib.Entities.DTO.Game;

namespace modLib.Controllers
{
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly GameService _service;

        public GameController(ILogger<GameController> logger, GameService service)
        {
            _logger = logger;
            _service = service;
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


    }
}
