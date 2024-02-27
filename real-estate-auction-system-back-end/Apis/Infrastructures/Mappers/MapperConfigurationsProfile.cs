﻿using Application.ViewModels.ChemicalsViewModels;
using AutoMapper;
using Application.Commons;
using Domain.Entities;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
        }
    }
}
