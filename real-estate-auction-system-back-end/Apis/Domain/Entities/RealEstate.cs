using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RealEstate : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double StartPrice { get; set; }
        public double Acreage { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Description { get; set; }
        public DateTime DateSubmited { get; set; }
        public bool IsAvailable { get; set; }
        public virtual IList<RealEstateImage> RealEstateImages { get; set; }
        public int AccountId { get; set; }
        public int TypeOfRealEstateId { get; set; }
        public virtual TypeOfRealEstate TypeOfRealEstate { get; set; }
        public int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }
    }
}
