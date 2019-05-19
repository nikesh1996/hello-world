using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class AuctionItem
    {
        public int SellerUid { get; set; }
        public int InitialQuantity { get; set; }
        public int FinalQuantity { get; set; }
        public string Item { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal BasePrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int AuctionId { get; set; }
        public int EmpId { get; set; }

    }
}
