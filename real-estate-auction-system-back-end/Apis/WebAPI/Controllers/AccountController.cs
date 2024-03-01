using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.UserViewModels;

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
        public async Task RegisterAsync(UserRegisterDTO userRegisterDTO) => await _accountService.RegisterAsync(userRegisterDTO);

        [HttpPost]
        public async Task<string> LoginAsync(UserLoginDTO loginObject) => await _accountService.LoginAsync(loginObject);
    }
}