using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231PE_SP24_123890_SE160385.DTO;
using PRN231PE_SP24_123890_SE160385PE_PRN231_FA23_TrialTest_Se160385_Client.Utils;
using Repositories.Repositories;

namespace PRN231PE_SP24_123890_SE160385.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountRepository userAccountRepository;
        public UserAccountController(IUserAccountRepository _userAccountRepository)
        {
            userAccountRepository = _userAccountRepository;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var response = new ApiResponse();
            try
            {
                var account = await userAccountRepository.Login(dto.Email, dto.Password);
                if (account == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Wrong username/password!";
                }
                else
                {
                    IConfiguration config = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();
                    var token = account.GenerateJsonWebToken(config["AppSettings:SecretKey"], DateTime.Now);
                    response.Success = true;
                    response.Value = token;
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return BadRequest(response);
        }
    }
}
