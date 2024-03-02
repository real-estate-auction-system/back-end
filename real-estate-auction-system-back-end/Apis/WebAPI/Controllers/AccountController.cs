using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.UserViewModels;
using Application.Commons;
using Application.Services;
using Application.ViewModels.RealEstateViewModels;
using WebAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;

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
    }
}