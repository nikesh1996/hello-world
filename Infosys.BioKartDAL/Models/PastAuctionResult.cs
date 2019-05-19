using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class PastAuctionResult
    {
        public int? AuctionId { get; set; }
        public int WinnerId { get; set; }
        public int FarmerId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime? EndDate { get; set; }
        public int AuctionResult { get; set; }

        public AuctionItem Auction { get; set; }
        public Users Farmer { get; set; }
        public Users Winner { get; set; }
    }
}
