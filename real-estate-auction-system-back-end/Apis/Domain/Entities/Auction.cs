using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Auction : BaseEntity
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AuctionStatus AuctionStatus { get; set; }
        public int CreatorId { get; set; }
        public int ManagedId { get; set; }
    }
}
