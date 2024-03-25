using Application;
using Application.Repositories;
using Application.ViewModels.NewsViewModel;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        private readonly AppDbContext _context;

        public NewsRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public void UpdateNewsModel(NewsModel newsModel)
        {
            _context.Set<NewsModel>().Attach(newsModel);
            _context.Entry(newsModel).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public NewsModel GetById(int id)
        {
            return _context.Set<NewsModel>().Find(id);
        }
    }
}
