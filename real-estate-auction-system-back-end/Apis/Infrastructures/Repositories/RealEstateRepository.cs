using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class RealEstateRepository : GenericRepository<RealEstate>, IRealEstateRepository
    {
        private readonly AppDbContext _dbContext;

        public RealEstateRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RealEstate>> GetAllRealEstates()
        {
            var realEstates = await _dbContext.RealEstates.Include(re => re.RealEstateImages).ToListAsync();
            return realEstates;
        }
        public async Task<RealEstate> GetEstates(int id)
        {
            var realEstates = await _dbContext.RealEstates.Where(x => x.Id == id).Include(re => re.RealEstateImages).FirstOrDefaultAsync();
            return realEstates;
        }
    }
}
