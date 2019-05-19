using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class Requests
    {
        public int Cid { get; set; }
        public int Quantity { get; set; }
        public string Item { get; set; }
        public int Rid { get; set; }
        public string ForwardStatus { get; set; }

        public Users C { get; set; }
    }
}
