using Application.Interfaces;
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
        public RealEstateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<RealEstate>> GetAll()
        {
            return await _unitOfWork.RealEstateRepository.GetAllAsync();
        }
    }
}
