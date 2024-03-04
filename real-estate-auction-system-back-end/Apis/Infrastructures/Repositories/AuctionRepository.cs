﻿using Application.Interfaces;
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
    public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
    {
        private readonly AppDbContext _dbContext;

        public AuctionRepository(AppDbContext dbContext, IClaimsService claimsService)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Auction?> GetTodayAuction()
        {
            var auction = await _dbContext.Auctions.Where(x => x.StartTime.Date == new DateTime(2024, 3, 3) /*DateTime.Now.Date*/).Include(x => x.RealEstates).FirstOrDefaultAsync();
            return auction;
        }
    }
}
