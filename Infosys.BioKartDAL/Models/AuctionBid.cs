using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class AuctionBid
    {
        public int? AuctionId { get; set; }
        public int BidderId { get; set; }
        public int BidId { get; set; }
        public DateTime? BidDate { get; set; }
        public decimal BidAmount { get; set; }

        public AuctionItem Auction { get; set; }
        public Users Bidder { get; set; }
    }
}
