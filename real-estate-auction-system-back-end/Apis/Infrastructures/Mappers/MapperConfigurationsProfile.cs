
using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModels.RealEstateViewModels;
using Application.ViewModels.NewsViewModel;
using Application.ViewModels.AuctionsViewModels;
using Application.ViewModels.OrderViewModel;
using Application.ViewModels.UserViewModels;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            CreateMap<Auction, AuctionModel>().ReverseMap();
            CreateMap<RealEstate, RealEstateModel>().ReverseMap();
            CreateMap<RealEstate, RealEstateUpdateRequest>().ReverseMap();
            CreateMap<News, NewsModel>().ReverseMap();

            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<UpdateAccountRequest, Account>();
            CreateMap<Order, OrderResponse>();
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<RealEstateModel, RealEstate>();
        }
    }
}
