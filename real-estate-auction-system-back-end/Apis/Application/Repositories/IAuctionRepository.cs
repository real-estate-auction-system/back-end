using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAuctionRepository : IGenericRepository<Auction>
    {
        Task<Auction?> GetTodayAuction();
        Task<List<Auction>> GetUpcomingAuctions();
    }
}
