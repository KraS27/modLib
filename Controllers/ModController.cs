using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using modLib.BL;
using modLib.Entities.DTO.Mods;
using modLib.Entities.Exceptions;
using modLib.Models.Entities;

namespace modLib.Controllers
{
    [ApiController]
    public class ModController : ControllerBase
    {       
        private readonly ModService _service;
        private readonly IValidator<CreateModDTO> _createModDTOValidator;
        private readonly IValidator<UpdateModDTO> _updateModDTOValidator;

        public ModController(ModService repository, 
            IValidator<CreateModDTO> createModDTOValidator, 
            IValidator<UpdateModDTO> updateModDTOValidator)
        {
            _service = repository;
            _createModDTOValidator = createModDTOValidator;
            _updateModDTOValidator = updateModDTOValidator;
        }

        [HttpGet("mods/{id}")]
        public async Task<IActionResult> GetMod(int id)
        {
            try
            {
                var mod = await _service.GetAsync(id);

                return mod is null ? NoContent() : Ok(mod);
            }
            catch 
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }           
        }

        [HttpGet("mods")]
        public async Task<IActionResult> GetMods()
        {
            try
            {
                var mods = await _service.GetAllWithGamesAsync();

                return Ok(mods);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }               
        }

        [HttpPost("mods")]
        public async Task<IActionResult> AddMod([FromBody] CreateModDTO modDTO)
        {
            try
            {
                var validationResult = await _createModDTOValidator.ValidateAsync(modDTO);
                if (!validationResult.IsValid)             
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                
                   
                await _service.CreateAsync(modDTO);

                return Ok();
            }
            catch (AlreadyExistException ex )
            {
                return BadRequest(ex.Message);
            }
            catch (ForeignKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpPost("mods/range")]
        public async Task<IActionResult> AddMods([FromBody] List<CreateModDTO> modsDTO)
        {
            try
            {
                var validationResults = modsDTO.Select(x => _createModDTOValidator.Validate(x)).ToList();

                foreach(var validationResult in validationResults)
                {
                    if(!validationResult.IsValid)
                        return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                await _service.CreateRangeAsync(modsDTO);
                return Ok();
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ForeignKeyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("mods/{id}")]
        public async Task<IActionResult> RemoveMod(int id)
        {
            try
            {
                await _service.RemoveAsync(id);

                return Ok();
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }         
        }

        [HttpPut("mods")]
        public async Task<IActionResult> UpdateMod([FromBody] UpdateModDTO modModel)
        {
            try
            {
                var validationResult = await _updateModDTOValidator.ValidateAsync(modModel);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));

                await _service.UpdateDTOAsync(modModel);

                return Ok();
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
