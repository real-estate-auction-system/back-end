using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.OrderViewModel;
using Application.ViewModels.RealEstateViewModels;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<OrderResponse> CreateOrder(CreateOrderRequest request)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.CheckOrderExited(request.AccountId, request.RealEstateId);
                if (order.Equals(true))
                {
                    throw new Exception("Order exited please try again");
                }

                var r = _unitOfWork.RealEstateRepository.FindAsync(rr => rr.Id == request.RealEstateId);
                if (r == null)
                {
                    throw new Exception("Real estate not found!");
                }
                Order o = new Order();
                o.OrderDate = DateTime.Now;
                o.Price = request.Price;
                o.status = (Domain.Enums.OrderStatus)1;
                o.AccountId = request.AccountId;
                o.RealEstateId = request.RealEstateId;
                await _unitOfWork.OrderRepository.AddAsync(o);
                await _unitOfWork.SaveChangeAsync();


                return _mapper.Map<OrderResponse>(o);
            }
            catch (Exception ex)
            {
                throw new Exception("Create Order error!");
            }
        }

        public async Task<List<Order>> GetOrderByAccountId(int accountId)
        {
            try
            {
                
                var response = await _unitOfWork.OrderRepository.GetOrderById(accountId);
                if (response == null)
                {
                    throw new Exception("Không tìm thấy");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Get list order of account by id {accountId.ToString()} error!");
            }
        }

        public async Task<Pagination<OrderResponse>> GetOrders(int pageIndex, int pageSize)
        {
            try
            {
                var response = await _unitOfWork.OrderRepository.ToPagination(pageIndex, pageSize);
                List<OrderResponse> items = new List<OrderResponse>();
                foreach (Order o in response.Items)
                {
                    items.Add(_mapper.Map<OrderResponse>(o));
                }
                var pagination = new Pagination<OrderResponse>()
                {
                    Items = items,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalItemsCount = response.TotalItemsCount,
                };
                return pagination;
            }
            catch (Exception ex)
            {
                throw new Exception("Get list order error!");
            }
        }

        public async Task<OrderResponse> UpdateOrderStatus(int id)
        {
            try
            {
                Order order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                if (order == null)
                {
                    throw new Exception($"Not found account with id {id.ToString()}");
                }
                order.status = (Domain.Enums.OrderStatus)2;

                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<OrderResponse>(order);
            }
            catch (Exception ex)
            {
                throw new Exception("Update order error!");
            }
        }
    }
}
