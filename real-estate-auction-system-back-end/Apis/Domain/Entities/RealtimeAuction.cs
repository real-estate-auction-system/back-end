using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RealtimeAuction : BaseEntity
    {
        public double StartingPrice { get; set; }
        public double CurrentPrice { get; set; }
        public int RealEstateId { get; set; }
        public virtual RealEstate RealEstate { get; set; }
        public int AuctionId { get; set; }
        public virtual Auction Auction { get; set; }
    }
}
