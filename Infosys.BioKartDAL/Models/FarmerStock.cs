using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class FarmerStock
    {
        public int Uid { get; set; }
        public int Quantity { get; set; }
        public string Item { get; set; }
        public decimal PricePerUnit { get; set; }
        public string Pincode { get; set; }
        public int? CategoryId { get; set; }

        public Categories Category { get; set; }
        public Users U { get; set; }
    }
}
