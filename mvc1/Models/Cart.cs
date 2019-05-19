using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class Cart
    {
        public int Cartid { get; set; }
        public int Buyer { get; set; }
        public int Seller { get; set; }
        public decimal PricePerUnit { get; set; }
        public string ItemName { get; set; }
    }


}
