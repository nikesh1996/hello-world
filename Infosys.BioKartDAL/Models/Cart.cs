using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class Cart
    {
        public int Cartid { get; set; }
        public int? Buyer { get; set; }
        public int? Seller { get; set; }
        public decimal PricePerUnit { get; set; }
        public string ItemName { get; set; }
    }
}
