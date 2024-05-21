using Microsoft.AspNetCore.Mvc;
using modLib.BL;

namespace modLib.Controllers
{
    public class GameController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly GameService _service;

        public GameController(ILogger logger, GameService service)
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


    }
}
