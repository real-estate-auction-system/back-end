using Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.ViewModels.UserViewModels;
using Infrastructures.Services;
using Domain.Entities;
using Infrastructures.Repositories;
using Application.ViewModels.RealEstateViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstateController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;
        private readonly IClaimsService _claimsService;
        private readonly IConfiguration _configuration;
        public RealEstateController(IRealEstateService realEstateService, IClaimsService claimsService, IConfiguration configuration)
        {
            _realEstateService = realEstateService;
            _claimsService = claimsService;
            _configuration = configuration;
        }


    }
}