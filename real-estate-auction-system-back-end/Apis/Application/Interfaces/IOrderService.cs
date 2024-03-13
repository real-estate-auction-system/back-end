using Application.Commons;
using Application.ViewModels.OrderViewModel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task AddAsync(Order order);
        Task<Pagination<OrderResponse>> GetOrders(int pageIndex, int pageSize);
        Task<Pagination<OrderResponse>> GetOrderByAccountId(int accountId, int pageIndex, int pageSize);
        Task<OrderResponse> UpdateOrderStatus(int id);
        Task<OrderResponse> CreateOrder(CreateOrderRequest request);

    }
}
