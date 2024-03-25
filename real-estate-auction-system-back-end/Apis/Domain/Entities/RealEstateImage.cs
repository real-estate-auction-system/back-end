using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RealEstateImage : BaseEntity
    {
        public string ImageURL { get; set; }
        public int RealEstateId { get; set; }


        [JsonIgnore]
        public virtual RealEstate RealEstate { get; set; }
    }
}
