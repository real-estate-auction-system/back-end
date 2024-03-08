using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AuctionsViewModels
{
    public class AuctionModel
    {
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public AuctionStatus AuctionStatus { get; set; }
        public int? CreatorId { get; set; }
        public int? ManagedId { get; set; }
    }
}
