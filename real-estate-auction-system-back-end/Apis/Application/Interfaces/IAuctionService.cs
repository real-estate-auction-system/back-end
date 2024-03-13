using Application.ViewModels.AuctionsViewModels;
using Application.ViewModels.RealEstateViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuctionService
    {
        Task<Auction?> GetTodayAuction();
        Task AddAsync(AuctionModel auctionModel, int userId);

        Task<List<Auction>> GetUpcomingAuctions();
    }
}
