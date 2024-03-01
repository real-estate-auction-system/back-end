using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.NewsViewModel;
using Application.ViewModels.RealEstateViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NewsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<News>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var news = await _unitOfWork.NewsRepository.ToPagination(pageIndex, pageSize);
            return news;
        }
        public async Task AddAsync(NewsModel newsModel, int userId)
        {
            var news = _mapper.Map<News>(newsModel);
            if (news == null)
            {
                throw new ArgumentNullException(nameof(news));
            }
            news.time = DateTime.Now;
            news.AccountId = userId;
            await _unitOfWork.NewsRepository.AddAsync(news);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
