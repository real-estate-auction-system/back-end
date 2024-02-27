using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RealEstateImage : BaseEntity
    {
        public string ImageURL { get; set; }
        public int RealEstateId { get; set; }
        public virtual RealEstate RealEstate { get; set; }
    }
}
