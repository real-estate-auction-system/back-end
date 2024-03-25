﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.RealEstateViewModels
{
    public class RealEstateUpdateRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double StartPrice { get; set; }
        public double Acreage { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public int AuctionId { get; set; }
        public DateTime DateSubmited { get; set; }
        public int TypeOfRealEstateId { get; set; }
        
        public List<IFormFile>? Image { get; set; } = default!;


    }
}
