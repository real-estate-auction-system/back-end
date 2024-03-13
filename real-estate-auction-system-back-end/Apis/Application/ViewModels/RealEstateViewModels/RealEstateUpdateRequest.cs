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
        public string Description { get; set; }
        public DateTime DateSubmited { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

    }
}
