using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Firebase.Auth.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuctionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Auction?> GetTodayAuction()
        {
            var auction = await _unitOfWork.AuctionRepository.GetTodayAuction();
            return auction;
        }
    }
}
