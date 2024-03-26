using Application.Commons;
using Application.ViewModels.AuctionsViewModels;
using Application.ViewModels.RealEstateViewModels;
using Application.ViewModels.UserViewModels;
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

        Task<List<AuctionResponse>> GetAllAuctions();
        Task<Pagination<AuctionResponse>> GetAuctions(int pageIndex, int pageSize);
        Task<AuctionResponse> UpdateAuction(int id, AuctionResponse request);
        Task<dynamic> DeleteAuction(int id);
        Task<AuctionResponse> CreateAuction(AuctionRequest request);

        Task<Auction> GetAuctionById(int id);
    }
}
