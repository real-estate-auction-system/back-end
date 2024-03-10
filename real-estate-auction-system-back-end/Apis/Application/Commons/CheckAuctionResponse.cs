using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons
{
    public class CheckAuctionResponse
    {
        public bool OnGoing { get; set; }
        public double CurrentPrice { get; set; }
        public DateTime Endtime { get; set; }
        public double UserBind { get; set; }
    }
}
