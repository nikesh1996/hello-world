using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class AuctionCart
    {
        public int Cartid { get; set; }
        public int AuctionId { get; set; }
        public int? Buyer { get; set; }
        public int? Seller { get; set; }
        public decimal TotalPrice { get; set; }
        public string ItemName { get; set; }
    }
}
