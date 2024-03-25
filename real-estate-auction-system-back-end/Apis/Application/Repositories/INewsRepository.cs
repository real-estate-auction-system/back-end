using Application.ViewModels.NewsViewModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface INewsRepository : IGenericRepository<News>
    {
        void UpdateNewsModel(NewsModel newsModel);

        NewsModel GetById(int id);
    }
}
