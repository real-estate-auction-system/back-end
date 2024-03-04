﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRealtimeAuctionService
    {
        Task AddRealtimeAuction(int realEstateId, double currentPrice, int accountId);
        Task StartAuction(int realEstateId);
    }
}
