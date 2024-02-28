using Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.ViewModels.UserViewModels;
using Infrastructures.Services;
using Domain.Entities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;
        public RealEstateController(IRealEstateService realEstateService)
        {
            _realEstateService = realEstateService;
        }

        [HttpGet]
        public async Task<List<RealEstate>> GetAll()
        {
            var list = await _realEstateService.GetAll();
            if (list.Count == 0)
            {
                throw new Exception("Khong co gi");
            }
            return list;
        }
    }
}
