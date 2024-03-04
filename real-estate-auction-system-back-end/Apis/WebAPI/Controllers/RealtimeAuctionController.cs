using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Hubs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealtimeAuctionController : ControllerBase
    {
        private readonly IRealtimeAuctionService _realtimeAuctionService;
        private readonly IRealEstateService _realEstateService;
        private readonly IClaimsService _claimsService;
        private readonly IHubContext<AuctionHub> _hubContext;
        public static int AuctionDurationSeconds;
        public static double CurrentPrice = 0;
        public RealtimeAuctionController(IRealtimeAuctionService realtimeAuctionService, IClaimsService claimsService, IHubContext<AuctionHub> hubContext, IRealEstateService realEstateService)
        {
            _realtimeAuctionService = realtimeAuctionService;
            _claimsService = claimsService;
            _hubContext = hubContext;
            _realEstateService = realEstateService;
        }

        [HttpPut("StartAuction")]
        public async Task<IActionResult> StartAuction(int realEstateId)
        {
            try
            {
                var realEstate = await _realEstateService.GetByIdAsync(realEstateId);
                if (realEstate == null)
                {
                    throw new Exception("Khong tim thay san pham");
                }
                await _hubContext.Clients.All.SendAsync("AuctionStarted", AuctionDurationSeconds, CurrentPrice);
                await _realtimeAuctionService.StartAuction(realEstateId);
                AuctionDurationSeconds = 30;
                CurrentPrice = 0;
                while (AuctionDurationSeconds > 0)
                {
                    await _hubContext.Clients.All.SendAsync("AuctionCountdown", AuctionDurationSeconds, CurrentPrice);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    AuctionDurationSeconds--;

                }
                if (CurrentPrice != 0)
                {
                    //get the real auction where its price = current price
                    //make a new order here
                }
                await _hubContext.Clients.All.SendAsync("AuctionEnded", AuctionDurationSeconds, CurrentPrice);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddRealtimeAuction(int realEstateId, double currentPrice)
        {
            AuctionDurationSeconds = 15;
            CurrentPrice = currentPrice;
            await _realtimeAuctionService.AddRealtimeAuction(realEstateId, currentPrice, _claimsService.GetCurrentUserId);  
            return Ok();
        }
    }
}
