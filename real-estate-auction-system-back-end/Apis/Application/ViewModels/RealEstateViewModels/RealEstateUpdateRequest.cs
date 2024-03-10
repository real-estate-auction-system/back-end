using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.RealEstateViewModels
{
    public class RealEstateUpdateRequest
    {
        public double Price { get; set; }
        public double StartPrice { get; set; }
        public double Acreage { get; set; }
        public string Description { get; set; }
    }
}
