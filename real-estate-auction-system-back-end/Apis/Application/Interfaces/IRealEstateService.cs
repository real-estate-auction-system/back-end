using Application.Commons;
using Application.ViewModels.RealEstateViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRealEstateService
    {
        Task<List<RealEstate>> GetAll();
        Task AddAsync(RealEstateModel realEstateModel, int userId);
        Task<RealEstate?> UpdateAsync(int id, RealEstate realEstate);
        Task Update(RealEstate realEstate);
        Task DeleteAsync(RealEstate realEstate);
        Task<RealEstate?> GetByIdAsync(int id);

        Task<Pagination<RealEstate>> GetRealEstateByType(int pageIndex, int pageSize, int typeId);
        Task<Pagination<RealEstate>> GetRealEstateByName(int pageIndex, int pageSize, string name);
        Task<RealEstate> UpdateRealEstate(RealEstateUpdateRequest request, int Id);

    }
}
