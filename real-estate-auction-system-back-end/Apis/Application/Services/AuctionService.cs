using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.AuctionsViewModels;
using Application.ViewModels.OrderViewModel;
using Application.ViewModels.RealEstateViewModels;
using Application.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Entities;
using Firebase.Auth.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuctionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(AuctionModel auctionModel, int userId)
        {
            var auction = _mapper.Map<Auction>(auctionModel);
            if (auction == null)
            {
                throw new ArgumentNullException(nameof(auction));
            }
            auction.CreatorId = userId;
            await _unitOfWork.AuctionRepository.AddAsync(auction);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<dynamic> DeleteAuction(int id)
        {
            try
            {
                var response = await _unitOfWork.AuctionRepository.GetByIdAsync(id);
                if (response == null)
                {
                    throw new Exception($"Not found account with id {id.ToString()}");
                }
                response.AuctionStatus = (Domain.Enums.AuctionStatus)2;
                _unitOfWork.AuctionRepository.Update(response);
                await _unitOfWork.SaveChangeAsync();
                return "Delete auction success";
            } catch (Exception ex)
            {
                throw new Exception("Get list auction error!");
            }
        }

        public async Task<Pagination<AuctionResponse>> GetAuctions(int pageIndex, int pageSize)
        {
            try
            {
                var response = await _unitOfWork.AuctionRepository.ToPagination(pageIndex, pageSize);
                List<AuctionResponse> items = new List<AuctionResponse>();
                foreach (var o in response.Items)
                {
                    if (o.AuctionStatus != (Domain.Enums.AuctionStatus)2)
                    {
                        items.Add(_mapper.Map<AuctionResponse>(o));
                    }
                    }
                var pagination = new Pagination<AuctionResponse>()
                {
                    Items = items,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalItemsCount = response.TotalItemsCount,
                };
                return pagination;
            }
            catch (Exception ex)
            {
                throw new Exception("Get list auction error!");
            }
        }

        public async Task<List<AuctionResponse>> GetAllAuctions()
        {
            try
            {
                var response = await _unitOfWork.AuctionRepository.GetAllAsync();
                List<AuctionResponse> items = new List<AuctionResponse>();
                if (response == null)
                {
                    throw new Exception("List auction is empty!");
                }
                else
                {
                    foreach (var a in response)
                    {
                        if (a.AuctionStatus != (Domain.Enums.AuctionStatus)2)
                        {
                            items.Add(_mapper.Map<AuctionResponse>(a));
                        }
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Get list auction error!");
            }
        }

        public async Task<Auction?> GetTodayAuction()
        {
            var auction = await _unitOfWork.AuctionRepository.GetTodayAuction();
            return auction;
        }

        public async Task<List<Auction>> GetUpcomingAuctions()
        {
            var auctions = await _unitOfWork.AuctionRepository.GetUpcomingAuctions();
            return auctions;
        }

        public async Task<AuctionResponse> UpdateAuction(int id, AuctionResponse request)
        {
            try
            {
                Auction a = await _unitOfWork.AuctionRepository.GetByIdAsync(id);
                if (a == null)
                {
                    throw new Exception($"Not found auction with id {id.ToString()}");
                }
                var existingTitle = _unitOfWork.AuctionRepository.FindAsync(a => a.Title.Equals(request.Title));
                if (existingTitle.Equals(true))
                {
                    throw new Exception("Title has already been taken");
                }

                _mapper.Map<AuctionResponse, Auction>(request, a);
                _unitOfWork.AuctionRepository.Update(a);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<AuctionResponse>(a);
            }
            catch (Exception ex)
            {
                throw new Exception("Update auction error!");
            }
        }

        public async Task<AuctionResponse> CreateAuction(AuctionResponse request)
        {
            try
            {
                Auction auction = new Auction();
                var existingTitle = _unitOfWork.AuctionRepository.FindAsync(a => a.Title.Equals(request.Title));
                if (existingTitle.Equals(true))
                {
                    throw new Exception("Title has already been taken");
                }
                auction.Title = request.Title;
                auction.Date = request.Date;
                auction.AuctionStatus = (Domain.Enums.AuctionStatus)1;
                auction.CreatorId = _unitOfWork.AccountRepository.FindAsync(a => a.RoleId == 1).Id;
                auction.ManagedId = _unitOfWork.AccountRepository.FindAsync(a => a.RoleId == 1).Id;

                await _unitOfWork.AuctionRepository.AddAsync(auction);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<AuctionResponse>(auction);
            }
            catch (Exception ex)
            {
                throw new Exception("Create auction error!"); 
            }
        }
    }
}
