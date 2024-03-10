﻿using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class RealEstateImageRepository : GenericRepository<RealEstateImage>, IRealEstateImageRepository
    {
        public RealEstateImageRepository(AppDbContext dbContext)
           : base(dbContext)
        {
        }
    }
}
