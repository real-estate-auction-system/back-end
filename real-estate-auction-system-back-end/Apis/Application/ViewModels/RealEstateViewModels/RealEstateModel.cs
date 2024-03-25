using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
<<<<<<< HEAD
        [NotMapped]        
=======
        public DateTime DateSubmited { get; set; }    
>>>>>>> 5854904ae19c2971cb9618594e9238610640cac8
        public List<IFormFile>? Image { get; set; } = default!;
    }
}
