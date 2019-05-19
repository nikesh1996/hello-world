using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class Auctionstock
    {
        public int Uid { get; set; }
        public int Quantity { get; set; }
        public string Item { get; set; }
        public decimal PricePerUnit { get; set; }
        public int? CategoryId { get; set; }
    }
}
