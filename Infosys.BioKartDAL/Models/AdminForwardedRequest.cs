using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class AdminForwardedRequest
    {
        public int Cid { get; set; }
        public int? CustomerRid { get; set; }
        public int Quantity { get; set; }
        public string Item { get; set; }
        public int Arid { get; set; }
        public int? FarmerId { get; set; }
        public string Status { get; set; }

        public Users C { get; set; }
    }
}
