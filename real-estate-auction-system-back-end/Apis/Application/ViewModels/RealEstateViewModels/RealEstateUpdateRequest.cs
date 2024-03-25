using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
<<<<<<< HEAD
        [NotMapped]
        public List<IFormFile>? Image { get; set; } = default!;
=======
        public int AuctionId { get; set; }
        public DateTime DateSubmited { get; set; }
        public int TypeOfRealEstateId { get; set; }
        
        public List<IFormFile>? Image { get; set; } = default!;

>>>>>>> 5854904ae19c2971cb9618594e9238610640cac8

    }
}
