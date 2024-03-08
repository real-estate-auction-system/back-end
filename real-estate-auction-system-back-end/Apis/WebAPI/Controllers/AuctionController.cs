using Application.Commons;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.AuctionsViewModels;
using Application.ViewModels.RealEstateViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly IClaimsService _claimsService;

        public AuctionController(IAuctionService auctionService, IClaimsService claimsService)
        {
            _auctionService = auctionService;
            _claimsService = claimsService;
        }

        [HttpGet("TodayAuction")]
        public async Task<IActionResult> GetAll()
        {
            var auction = await _auctionService.GetTodayAuction();
            if (auction == null)
            {
                throw new Exception("There is no auction for today!");
            }
            return Ok(auction);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuctionModel auctionModel)
        {
            try
            {
                await _auctionService.AddAsync(auctionModel, _claimsService.GetCurrentUserId);
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
