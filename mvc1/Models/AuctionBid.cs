using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class AuctionBid
    {
        public int AuctionId { get; set; }
        public int BidderId { get; set; }
        public int BidId { get; set; }
        public DateTime? BidDate { get; set; }
        public decimal BidAmount { get; set; }

    }
}
