using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class Request
    {
        public int CId { get; set; }
        public int Quantity { get; set; }
        public string Item { get; set; }
        public int RId { get; set; }
        public string ForwardStatus { get; set; }
    }
}
