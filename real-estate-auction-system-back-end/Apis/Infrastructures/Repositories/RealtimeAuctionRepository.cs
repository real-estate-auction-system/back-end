using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class RealtimeAuctionRepository : GenericRepository<RealtimeAuction>, IRealtimeAuctionRepository
    {
        public RealtimeAuctionRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
