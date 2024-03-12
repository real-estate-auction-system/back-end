using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.OrderViewModel
{
    public class OrderResponse
    {
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public OrderStatus status { get; set; }
        public string? UserName { get; set; }
        public string? RealEstateName { get; set; }
    }
}
