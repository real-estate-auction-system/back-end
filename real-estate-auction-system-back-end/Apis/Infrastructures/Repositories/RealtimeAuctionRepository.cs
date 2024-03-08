using Application;
using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class RealtimeAuctionRepository : GenericRepository<RealtimeAuction>, IRealtimeAuctionRepository
    {
        private readonly AppDbContext _dbContext;

        public RealtimeAuctionRepository(AppDbContext dbContext, IClaimsService claimsService)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RealtimeAuction> GetLastAuction(int realEstateId, double finalPrice)
        {

            var auction = await _dbContext.RealtimeAuctions.Where(x => x.RealEstateId == realEstateId && x.CurrentPrice == finalPrice).ToListAsync();
            return auction[0];
        }

        public async Task<RealtimeAuction?> GetLastAuctionOfUserId(int realEstateId, int accountId)
        {

            var auction = await _dbContext.RealtimeAuctions.Where(x => x.RealEstateId == realEstateId && x.AccountId == accountId).OrderByDescending(x => x.CurrentPrice).FirstOrDefaultAsync();
            return auction;
        }
    }
}
