using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.UserViewModels;
using Application.Commons;
using Application.Services;
using Application.ViewModels.RealEstateViewModels;
using WebAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserRegisterDTO userRegisterDTO){
           await _accountService.RegisterAsync(userRegisterDTO);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLoginDTO loginObject) {
            try
            {
                var response = await _accountService.LoginAsync(loginObject);
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

        [HttpGet]
        public async Task<IActionResult> GetAccounts([FromRoute] int pageIndex, int pageSize)
        {
            try
            {
                var response = await _accountService.GetAccounts(pageIndex, pageSize);
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

        [HttpGet("/api/accounts/getById/{id:int}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                var response = await _accountService.GetAccountById(id);
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
        public async Task<ActionResult<AccountResponse>> UpdateAccount([FromBody] UpdateAccountRequest request, int id)
        {
            var rs = await _accountService.UpdateAccount(id, request);
            if (rs == null) return NotFound();
            return Ok(rs);
        }

    }
}