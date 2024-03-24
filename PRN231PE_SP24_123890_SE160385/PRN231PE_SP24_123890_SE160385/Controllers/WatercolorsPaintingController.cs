using DataAccessObject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories.Repositories;
using System.ComponentModel.DataAnnotations;

namespace PRN231PE_SP24_123890_SE160385.Controllers
{
    [EnableQuery]
    [Authorize]
    public class WatercolorsPaintingController : ODataController
    {
        private readonly IWatercolorsPaintingRepository watercolorsPaintingRepository;
        public WatercolorsPaintingController(IWatercolorsPaintingRepository _watercolorsPaintingRepository)
        {
            watercolorsPaintingRepository = _watercolorsPaintingRepository;
        }
        [HttpGet]
        [Authorize(Roles = "3,2")]
        public ActionResult<IQueryable<WatercolorsPainting>> Get()
        {
            IQueryable<WatercolorsPainting> watercolorsPaintings;
            try
            {
                watercolorsPaintings = watercolorsPaintingRepository.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(watercolorsPaintings);
        }
        [HttpGet]
        [Authorize(Roles = "3")]
        public ActionResult<SingleResult> Get([FromRoute] string key)
        {
            var watercolorsPaintings = watercolorsPaintingRepository.GetById(key);

            if (watercolorsPaintings == null)
            {
                return NotFound();
            }
            return Ok(new SingleResult<WatercolorsPainting>(watercolorsPaintings));
        }
        [HttpPost]
        [Authorize(Roles = "3")]
        public async Task<ActionResult<WatercolorsPainting>> Post([FromBody] WatercolorsPainting watercolorsPainting)
        {
            var validationContext = new ValidationContext(watercolorsPainting);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(watercolorsPainting, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    throw new Exception(validationResult.ErrorMessage)!;
                }
            }
            try
            {
                await watercolorsPaintingRepository.AddAsync(watercolorsPainting);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("Get", new { key = watercolorsPainting.PaintingId }, watercolorsPainting);
        }
        [HttpPut]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Put([FromRoute] string key, [FromBody] WatercolorsPainting watercolorsPainting)
        {
            var validationContext = new ValidationContext(watercolorsPainting);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(watercolorsPainting, validationContext, validationResults, true))
            {
                foreach (var validationResult in validationResults)
                {
                    throw new Exception(validationResult.ErrorMessage)!;
                }
            }
            try
            {
                watercolorsPainting.PaintingId = key;
                await watercolorsPaintingRepository.UpdateAsync(watercolorsPainting);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Delete([FromRoute] string key)
        {
            try
            {
                await watercolorsPaintingRepository.DeleteAsync(key);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
