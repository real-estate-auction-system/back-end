using Application.Commons;
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
                var realTimeAuction = await _realtimeAuctionService.GetLastAuctionOfUserId(realEstateId, _claimsService.GetCurrentUserId);
                double myBind = 0;
                if (realTimeAuction != null)
                {
                    myBind = realTimeAuction.CurrentPrice;
                }
                if (realEstate.RealEstateStatus == Domain.Enums.RealEstateStatus.onGoing)
                    return Ok( new CheckAuctionResponse{ 
                    OnGoing = true,
                    CurrentPrice = CurrentPrice,
                    Endtime = realEstate.EndTime.Value,
                    UserBind = myBind
                    });
                throw new Exception("Da ket thuc");
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
                realEstate.StartTime = DateTime.Now;
                realEstate.EndTime = DateTime.Now.AddSeconds(30);
                await _realEstateService.Update(realEstate);
                await _hubContext.Clients.All.SendAsync("AuctionStarted", CurrentPrice);
                await _realtimeAuctionService.StartAuction(realEstateId);
                CurrentPrice = realEstate.StartPrice;
                while (DateTime.Now != realEstate.EndTime)
                {
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
                await _hubContext.Clients.All.SendAsync("AuctionEnded", CurrentPrice);
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
            CurrentPrice = currentPrice;
            await _realtimeAuctionService.AddRealtimeAuction(realEstateId, currentPrice, _claimsService.GetCurrentUserId);
            await _hubContext.Clients.All.SendAsync("AuctionCountdown", CurrentPrice);
            return Ok();
        }
    }
}
