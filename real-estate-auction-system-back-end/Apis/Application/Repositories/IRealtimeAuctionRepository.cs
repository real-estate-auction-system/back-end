using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IRealtimeAuctionRepository : IGenericRepository<RealtimeAuction>
    {
        Task<RealtimeAuction> GetLastAuction(int realEstateId, double finalPrice);
        Task<RealtimeAuction?> GetLastAuctionOfUserId(int realEstateId, int accountId);
    }
}
