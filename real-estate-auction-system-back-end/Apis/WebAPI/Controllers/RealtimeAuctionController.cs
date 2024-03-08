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
        private readonly IOrderService _orderService;
        private readonly IClaimsService _claimsService;
        private readonly IHubContext<AuctionHub> _hubContext;
        public static int AuctionDurationSeconds;
        public static double CurrentPrice = 0;
        public RealtimeAuctionController(IRealtimeAuctionService realtimeAuctionService, IOrderService orderService, IClaimsService claimsService, IHubContext<AuctionHub> hubContext, IRealEstateService realEstateService)
        {
            _realtimeAuctionService = realtimeAuctionService;
            _orderService = orderService;
            _claimsService = claimsService;
            _hubContext = hubContext;
            _realEstateService = realEstateService;
        }

        [HttpGet("StartAuction")]
        public async Task<IActionResult> CheckAuction(int realEstateId)
        {
            try
            {
                var realEstate = await _realEstateService.GetByIdAsync(realEstateId);
                if (realEstate == null)
                {
                    throw new Exception("Khong tim thay san pham");
                }
                if (realEstate.RealEstateStatus == Domain.Enums.RealEstateStatus.onGoing)
                    return Ok(true);
                else
                    return Ok(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
                AuctionDurationSeconds = 60;
                CurrentPrice = realEstate.StartPrice;
                while (AuctionDurationSeconds > 0)
                {
                    await _hubContext.Clients.All.SendAsync("AuctionCountdown", AuctionDurationSeconds, CurrentPrice);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    AuctionDurationSeconds--;
                }
                if (CurrentPrice != realEstate.StartPrice)
                {
                    var lastAuction = await _realtimeAuctionService.GetLastAuction(realEstateId, CurrentPrice);
                    await _orderService.AddAsync(new Order { 
                        AccountId = lastAuction.AccountId,
                        OrderDate = DateTime.Now,
                         Price = lastAuction.CurrentPrice,
                         status = Domain.Enums.OrderStatus.waiting,
                         RealEstateId = realEstateId
                    });
                    realEstate.RealEstateStatus = Domain.Enums.RealEstateStatus.finished;
                    await _realEstateService.Update(realEstate);
                }
                else
                {
                    realEstate.RealEstateStatus = Domain.Enums.RealEstateStatus.noUserBuy;
                    await _realEstateService.Update(realEstate);
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
            AuctionDurationSeconds = 30;
            CurrentPrice = currentPrice;
            await _realtimeAuctionService.AddRealtimeAuction(realEstateId, currentPrice, _claimsService.GetCurrentUserId);
            return Ok();
        }
    }
}
