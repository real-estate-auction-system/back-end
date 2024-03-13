using Application.Interfaces;
using Application.ViewModels.AuctionsViewModels;
using Application.ViewModels.RealEstateViewModels;
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

        public async Task AddAsync(AuctionModel auctionModel, int userId)
        {
            var auction = _mapper.Map<Auction>(auctionModel);
            if (auction == null)
            {
                throw new ArgumentNullException(nameof(auction));
            }
            auction.CreatorId = userId;
            await _unitOfWork.AuctionRepository.AddAsync(auction);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<Auction?> GetTodayAuction()
        {
            var auction = await _unitOfWork.AuctionRepository.GetTodayAuction();
            return auction;
        }

        public async Task<List<Auction>> GetUpcomingAuctions()
        {
            var auctions = await _unitOfWork.AuctionRepository.GetUpcomingAuctions();
            return auctions;
        }
    }
}
