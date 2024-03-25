using Application.Interfaces;
using Application.Repositories;
using Application.Commons;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Infrastructures.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }
        public Task<List<TEntity>> GetAllAsync() => _dbSet.ToListAsync();

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            // todo should throw exception when not found
            return result;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return ;
        }

        public void SoftRemove(TEntity entity)
        {
           _dbSet.Remove(entity);
        }


        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void SoftRemoveRange(List<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public async Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10)
        {
            var itemCount = await _dbSet.CountAsync();
            var items = await _dbSet.Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .AsNoTracking()
                                    .ToListAsync();
            
            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = itemCount,
                Items = items,
            };

            return result;
        }

        public void UpdateRange(List<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate) => _dbSet.SingleOrDefaultAsync(predicate);

        public IQueryable<TEntity> FindAll(Func<TEntity, bool> predicate) => _dbSet.Where(predicate).AsQueryable();

        public async Task Update(TEntity entity, int Id)
        {
            var existEntity = await GetById(Id);
            _dbSet.Entry(existEntity).CurrentValues.SetValues(entity);
            _dbSet.Update(existEntity);
        }
        public async Task<TEntity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
