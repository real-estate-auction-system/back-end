using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderViewModel
{
    public class CreateOrderRequest
    {
        public double Price { get; set; }
        public OrderStatus status { get; set; }
        public int AccountId { get; set; }
        public int RealEstateId { get; set; }
    }
}
