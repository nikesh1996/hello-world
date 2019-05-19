using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class Auctioncart
    {
        public int Cartid { get; set; }
        public int? AuctionId { get; set; }
        public int? Buyer { get; set; }
        public int? Seller { get; set; }
        public decimal TotalPrice { get; set; }
        public string ItemName { get; set; }
    }
}
