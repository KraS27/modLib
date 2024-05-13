﻿using Microsoft.AspNetCore.Mvc;
using modLib.BL;
using modLib.Models.Entities;

namespace modLib.Controllers
{
    [ApiController]
    public class ModController : ControllerBase
    {       
        private readonly ModsService _service;

        public ModController(ModsService repository)
        {
            _service = repository;
        }

        [HttpGet("mods/{id}")]
        public async Task<IActionResult> GetMod(Guid id)
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
                var mods = await _service.GetAllAsync();

                return Ok(mods);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }               
        }

        [HttpPost("mods")]
        public async Task<IActionResult> AddMod([FromBody] ModModel modModel)
        {
            try
            {
                await _service.CreateAsync(modModel);

                return Ok(modModel.Id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }            
        }

        [HttpDelete("mods/{id}")]
        public async Task<IActionResult> RemoveMod(Guid id)
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
        public async Task<IActionResult> UpdateMod([FromBody] ModModel modModel)
        {
            try
            {
                await _service.UpdateAsync(modModel);

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
