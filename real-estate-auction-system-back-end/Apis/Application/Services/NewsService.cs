﻿using Application.Commons;
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
        private readonly FirebaseService _firebaseService;

        public NewsService(IUnitOfWork unitOfWork, IMapper mapper, FirebaseService firebaseService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _firebaseService = firebaseService;
        }
        public async Task<Pagination<News>> GetPaginationAsync(int pageIndex, int pageSize)
        {
            var news = await _unitOfWork.NewsRepository.ToPagination(pageIndex, pageSize);
            return news;
        }
        public async Task AddAsync(NewsModel newsModel, int userId)
        {
            try
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

                // image
                if (newsModel.Image.Count != 0)
                {
                    foreach (var singleImage in newsModel.Image.Select((image, index) => (image, index)))
                    {
                        string newImageName = news.Id + "_i" + singleImage.index;
                        string folderName = $"news/{news.Id}/Image";
                        string imageExtension = Path.GetExtension(singleImage.image.FileName);
                        //Kiểm tra xem có phải là file ảnh không.
                        string[] validImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                        const long maxFileSize = 20 * 1024 * 1024;
                        if (Array.IndexOf(validImageExtensions, imageExtension.ToLower()) == -1 || singleImage.image.Length > maxFileSize)
                        {
                            throw new Exception("Có chứa file không phải ảnh hoặc quá dung lượng tối đa(>20MB)!");
                        }
                        var url = await _firebaseService.UploadFileToFirebaseStorage(singleImage.image, newImageName, folderName);
                        if (url == null)
                            throw new Exception("Lỗi khi đăng ảnh lên firebase!");

                        NewsImage newsImage = new NewsImage()
                        {
                            NewsId = news.Id,
                            ImageURL = url
                        };

                        await _unitOfWork.NewsImageRepository.AddAsync(newsImage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            //var result = new NewsModel();
            //var news = await _unitOfWork.NewsRepository.GetByIdAsync(id);
            //if (news != null)
            //{
            //    result.Name = news.Name;
            //    result.Title = news.Title;
            //    result.Description = news.Description;
            //    result.Image = news.I;
            //    //result.time = news.time;
            //}
            //else
            //{
            //    return null;
            //}
            //return result;
            return await _unitOfWork.NewsRepository.GetByIdAsync(id);
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
            //newsModelExisted.image = newsModel.Im;

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

        public async Task<NewsModel> GetNewsAsync(int id)
        {
            var result = new NewsModel();
            var news = await _unitOfWork.NewsRepository.GetByIdAsync(id);
            if (news != null)
            {
                result.Name = news.Name;
                result.Title = news.Title;
                result.Description = news.Description;

                //result.time = news.time;
            } else
            {
                return null;
            }
            return result;
            //return await _unitOfWork.NewsRepository.GetByIdAsync(id);
        }

    }
}
