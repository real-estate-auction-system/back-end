using Application.Interfaces;
using Application.Services;
using Application.ViewModels.OrderViewModel;
using Application.ViewModels.UserViewModels;
using Infrastructures.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IClaimsService _claimsService;
        public OrderController(IOrderService orderService, IClaimsService claimsService)
        {
            _orderService = orderService;
            _claimsService = claimsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            await _orderService.CreateOrder(request);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromRoute] int pageIndex, int pageSize)
        {
            try
            {
                var response = await _orderService.GetOrders(pageIndex, pageSize);
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

        [HttpGet("AccountId")]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var response = await _orderService.GetOrderByAccountId(_claimsService.GetCurrentUserId);
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
        public async Task<ActionResult<AccountResponse>> UpdateAccount(int id)
        {
            var rs = await _orderService.UpdateOrderStatus(id);
            if (rs == null) return NotFound();
            return Ok(rs);
        }
    }
}
