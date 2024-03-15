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
        public RealEstateRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

    }
}
