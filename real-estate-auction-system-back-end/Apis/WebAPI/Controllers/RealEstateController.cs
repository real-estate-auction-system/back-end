using Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.ViewModels.UserViewModels;
using Infrastructures.Services;
using Domain.Entities;
using Infrastructures.Repositories;
using Application.ViewModels.RealEstateViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;
        private readonly IClaimsService _claimsService;
        private readonly IConfiguration _configuration;
        public RealEstateController(IRealEstateService realEstateService, IClaimsService claimsService, IConfiguration configuration)
        {
            _realEstateService = realEstateService;
            _claimsService = claimsService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<List<RealEstate>> GetAll()
        {
            var list = await _realEstateService.GetAll();
            if (list.Count == 0)
            {
                throw new Exception("Khong co gi");
            }
            return list;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] RealEstateModel realEstateModel)
        {
            try
            {
                await _realEstateService.AddAsync(realEstateModel, _claimsService.GetCurrentUserId);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRealEstate(int id, [FromBody] RealEstate realEstate)
        {
            if (realEstate == null || id != realEstate.Id)
            {
                return BadRequest("Invalid request");
            }

            var existingRealEstate = _realEstateService.GetByIdAsync(id);
            if (existingRealEstate == null)
            {
                return NotFound();
            }

            _realEstateService.Update(realEstate);
            
            return NoContent();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteRealEstate([FromRoute]int id)
        {
            bool estateRemoved = false;
            return Ok();
        }
    }
}