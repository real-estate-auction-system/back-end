using Application.Commons;
using Application.Interfaces;
using Application.Services;
using Application.ViewModels.AuctionsViewModels;
using Application.ViewModels.RealEstateViewModels;
using Application.ViewModels.UserViewModels;
using Azure.Core;
using Domain.Entities;
using Infrastructures.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
        public async Task<IActionResult> GetTodayAuction()
        {
            try
            {
                var auction = await _auctionService.GetTodayAuction();
                if (auction == null)
                {
                    ModelState.AddModelError("firstError", "Hôm nay không có buổi đấu giá nào");
                    return ValidationProblem();
                }
                return Ok(auction);
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

        [HttpGet("UpcomingAuction")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> GetUpcomingAuction()
        {
            var auctions = await _auctionService.GetUpcomingAuctions();

            if (auctions.Count == 0)
            {
               ModelState.AddModelError("firstError", "Sắp tới không có buổi đấu giá nào");
                return ValidationProblem();
            }

            return Ok(auctions);
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

        [HttpPost("auction")]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> PostAuction([FromForm] AuctionRequest auction)
        {
            try
            {
                await _auctionService.CreateAuction(auction);
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

        [HttpGet]
        public async Task<IActionResult> GetAuctions([FromRoute] int pageIndex, int pageSize)
        {
            try
            {
                var response = await _auctionService.GetAuctions(pageIndex, pageSize);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("no-paging")]
        public async Task<IActionResult> GetAllAuctions()
        {
            try
            {
                var response = await _auctionService.GetAllAuctions();
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

        [HttpPut("{id:int}")]
        [Authorize(Roles = "1")]
        public async Task<ActionResult<AuctionResponse>> UpdateAuction([FromForm] AuctionResponse request, int id)
        {
            var rs = await _auctionService.UpdateAuction(id, request);
            if (rs == null) return NotFound();
            return Ok(rs);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            try
            {
                var rs = await _auctionService.DeleteAuction(id);
                if (rs == null) return NotFound();
                return Ok(rs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetAuctionById(int id)
        {
            try
            {
                var response = await _auctionService.GetAuctionById(id);
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
    }
}
