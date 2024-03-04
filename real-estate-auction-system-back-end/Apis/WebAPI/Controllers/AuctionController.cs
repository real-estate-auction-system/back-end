using Application.Commons;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
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
    }
}
