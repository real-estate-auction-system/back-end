using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.RealEstateViewModels
{
    public class RealEstateModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double StartPrice { get; set; }
        public double Acreage { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public string DateSubmited { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int TypeOfRealEstateId { get; set; }
    }
}
