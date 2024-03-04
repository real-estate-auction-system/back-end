using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RealtimeAuctionService : IRealtimeAuctionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RealtimeAuctionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task AddRealtimeAuction(int realEstateId, double currentPrice, int accountId)
        {
            var realEstate = await _unitOfWork.RealEstateRepository.GetByIdAsync(realEstateId);
            if (realEstate == null)
            {
                throw new Exception("Không tìm thấy bất động sản");
            }
            var realtimeAuction = new RealtimeAuction
            {
                RealEstateId = realEstateId,
                StartingPrice = realEstate.StartPrice,
                CurrentPrice = currentPrice,
                AccountId = accountId,
            };
            await _unitOfWork.RealtimeAuctionRepository.AddAsync(realtimeAuction);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task StartAuction(int realEstateId)
        {
            var realEstate = await _unitOfWork.RealEstateRepository.GetByIdAsync(realEstateId);
            if (realEstate == null)
            {
                throw new Exception("Không tìm thấy bất động sản");
            }
            realEstate.RealEstateStatus = Domain.Enums.RealEstateStatus.onGoing;
            _unitOfWork.RealEstateRepository.Update(realEstate);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<RealtimeAuction> GetLastAuction(int realEstateId, double finalPrice)
        => await _unitOfWork.RealtimeAuctionRepository.GetLastAuction(realEstateId, finalPrice);
    }
}
