using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class PastAuctionResult
    {
        public int? AuctionId { get; set; }
        public int WinnerId { get; set; }
        public int FarmerId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime? EndDate { get; set; }
        public int AuctionResult { get; set; }
    }
}
