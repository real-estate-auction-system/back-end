using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.NewsViewModel;
using Application.ViewModels.RealEstateViewModels;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        public async Task<NewsModel> GetNewsAsync(int id)
        {
            var result = new NewsModel();
            var news = await _unitOfWork.NewsRepository.GetByIdAsync(id);
            if (news != null)
            {
                result.Name = news.Name;
                result.Title = news.Title;
                result.Description = news.Description;
                result.image = news.image;
                //result.time = news.time;
            } else
            {
                return null;
            }
            return result;
            //return await _unitOfWork.NewsRepository.GetByIdAsync(id);
        }

        public async Task<News?> UpdateNewsModel(int id, News newsModel)
        {
            var newsModelExisted = await _unitOfWork.NewsRepository.GetByIdAsync(id);
            if (newsModelExisted == null)
            {
                return null;
            }
            newsModelExisted.Name = newsModel.Name;
            newsModelExisted.Title = newsModel.Title;
            newsModelExisted.Description = newsModel.Description;
            newsModelExisted.image = newsModel.image;

            _unitOfWork.NewsRepository.Update(newsModelExisted);
            await _unitOfWork.SaveChangeAsync();
            return newsModelExisted;
        }
        public async Task Update(News news)
        {
            _unitOfWork.NewsRepository.Update(news);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<News?> GetByIdAsync(int id)
        {
            return await _unitOfWork.NewsRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(News news)
        {
            _unitOfWork.NewsRepository.SoftRemove(news);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
