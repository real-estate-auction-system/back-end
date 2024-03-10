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
using Microsoft.AspNetCore.Server.IIS.Core;

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
        public async Task<List<RealEstate>> GetAllRealEstate()
        {
            var realEstates = await _realEstateService.GetAll();
            if (realEstates.Count == 0) { throw new Exception("Empty list ..."); }
            return realEstates;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRealEstate(int id)
        {
            try
            {
                var realEstate = await _realEstateService.GetByIdAsync(id);
                if (realEstate == null) { return NotFound(); }
                return Ok(realEstate);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]       
        public async Task<IActionResult> CreateRealEstate([FromForm] RealEstateModel realEstateModel)
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
            return Ok(realEstateModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRealEstate(int id, [FromBody] RealEstateUpdateRequest realEstateModel)
        {
          try
            {
                var realEstate = await _realEstateService.GetByIdAsync(id);
                if (realEstate == null) { return NotFound(); }
                realEstate.Price = realEstateModel.Price;
                realEstate.StartPrice = realEstateModel.StartPrice;
                realEstate.Acreage = realEstateModel.Acreage;
                realEstate.Description = realEstateModel.Description;
                await _realEstateService.Update(realEstate);
                return Ok(realEstateModel);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRealEstate(int id)
        {
            try
            {
                var realEstate = await _realEstateService.GetByIdAsync(id);
                if (realEstate == null) { return NotFound(); }
                await _realEstateService.DeleteAsync(realEstate);
                return Ok(realEstate);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}