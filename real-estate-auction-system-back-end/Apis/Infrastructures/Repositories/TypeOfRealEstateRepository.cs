using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class TypeOfRealEstateRepository : GenericRepository<TypeOfRealEstate>, ITypeOfRealEstateRepository
    {
        public TypeOfRealEstateRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}