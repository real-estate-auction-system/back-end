﻿using Application.Interfaces;
using Application.ViewModels.RealEstateViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RealEstateService : IRealEstateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RealEstateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(RealEstateModel realEstateModel, int userId)
        {
            var realEstate = _mapper.Map<RealEstate>(realEstateModel);
            if (realEstate == null)
            {
                throw new ArgumentNullException(nameof(realEstate));
            }
            realEstate.AccountId = userId;
            realEstate.AuctionId = 1;
            await _unitOfWork.RealEstateRepository.AddAsync(realEstate);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<RealEstate>> GetAll()
        {
            return await _unitOfWork.RealEstateRepository.GetAllAsync();
        }

        public Task<RealEstate?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(RealEstate realEstate) => _unitOfWork.RealEstateRepository.Update(realEstate);

    }
}
