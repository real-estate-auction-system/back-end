using Application.Interfaces;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        { 
            var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("Id");
            GetCurrentUserId = string.IsNullOrEmpty(Id) ? 0 : int.Parse(Id); ;
        }

        public int GetCurrentUserId { get; }

    }
}
