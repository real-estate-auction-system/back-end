using Application.Commons;
using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckOrderExited(int accountId, int realEstateId) => await _dbContext.Orders.AnyAsync(u => u.RealEstateId == realEstateId && u.AccountId == accountId);
        public async Task<List<Order>> GetOrderById(int accountId)
        {
            var orders = await _dbContext.Orders.Where(x => x.AccountId == accountId).Include(x => x.RealEstate).ToListAsync();
            return orders;
        }
    }
}
