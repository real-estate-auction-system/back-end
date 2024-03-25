using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<bool> CheckOrderExited(int accountId, int realEstateId);
        Task<List<Order>> GetOrderById(int accountId);
    }
}
