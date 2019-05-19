using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class AuctionItem
    {
        public AuctionItem()
        {
            AuctionBid = new HashSet<AuctionBid>();
            PastAuctionResult = new HashSet<PastAuctionResult>();
        }

        public int SellerUid { get; set; }
        public int FinalQuantity { get; set; }
        public string Item { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int AuctionId { get; set; }
        public int EmpId { get; set; }
        public string AuctionStatus { get; set; }

        public Users Emp { get; set; }
        public Users SellerU { get; set; }
        public ICollection<AuctionBid> AuctionBid { get; set; }
        public ICollection<PastAuctionResult> PastAuctionResult { get; set; }
    }
}
