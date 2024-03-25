using Application.Commons;
using Application.ViewModels.NewsViewModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INewsService
    {
        Task<Pagination<News>> GetPaginationAsync(int pageIndex, int pageSize);
        Task AddAsync(NewsModel newsModel, int userId);


        Task<News?> GetNewsByIdAsync(int id);



        Task Update(News news);

        Task<News?> UpdateNewsModel(int id ,News news);

        Task DeleteAsync(News news);

        Task<News?> GetByIdAsync(int id);
    }
}
