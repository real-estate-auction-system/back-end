
using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModels.RealEstateViewModels;
using Application.ViewModels.NewsViewModel;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            CreateMap<RealEstate, RealEstateModel>().ReverseMap();
            CreateMap<News, NewsModel>().ReverseMap();
        }
    }
}
