using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class AdminForwardedRequests
    {
        public int Cid { get; set; }
        public int? CustomerRid { get; set; }
        public int Quantity { get; set; }
        public string Item { get; set; }
        public int Arid { get; set; }
        public int? FarmerId { get; set; }
        public string Status { get; set; }

    }
}
