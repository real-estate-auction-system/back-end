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
using AutoMapper;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;
        private readonly IClaimsService _claimsService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public RealEstateController(IRealEstateService realEstateService, 
            IClaimsService claimsService, IConfiguration configuration, 
            IMapper mapper)
        {
            _realEstateService = realEstateService;
            _claimsService = claimsService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<RealEstate>> GetAllRealEstate()
        {
            var realEstates = await _realEstateService.GetAll();
            if (realEstates.Count == 0) { throw new Exception("Empty list ..."); }
            return realEstates;
        }

        [HttpGet("{id}", Name="GetRealEstate")]
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
<<<<<<< HEAD
        [Authorize(Roles = "1")]
=======
        [Authorize(Roles ="1")]
>>>>>>> 5854904ae19c2971cb9618594e9238610640cac8
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
            return CreatedAtRoute("GetRealEstate", realEstateModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> UpdateRealEstate(int id, [FromBody] RealEstateUpdateRequest realEstateModel)
        {
          try
            {
                
                var realEstateUpdate = await _realEstateService.UpdateAsync(id, realEstateModel);
                if(realEstateUpdate == null)
                {
                    return NotFound();  
                }
                return Ok(_mapper.Map<RealEstate>(realEstateUpdate));
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1")]
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

        [HttpGet("/api/realEstate/getByType/{id}")]
        public async Task<IActionResult> GetRealEstateByType(int id, [FromRoute] int pageIndex, int pageSize)
        {
            try
            {
                var response = await _realEstateService.GetRealEstateByType(pageIndex, pageSize, id);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/api/realEstate/getByName/{name}/name")]
        public async Task<IActionResult> GetRealEstateByName(string name, [FromRoute] int pageIndex, int pageSize)
        {
            try
            {
                var response = await _realEstateService.GetRealEstateByName(pageIndex, pageSize, name);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/api/realEstate/update/{id}/id")]
        public async Task<ActionResult<RealEstate>> UpdateRealEstate([FromBody] RealEstateUpdateRequest request, int id)
        {
            var rs = await _realEstateService.UpdateRealEstate(request, id);
            if (rs == null) return NotFound();
            return Ok(rs);
        }
    }
}