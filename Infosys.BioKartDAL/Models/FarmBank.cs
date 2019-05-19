using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class FarmBank
    {
        public decimal AccountNumber { get; set; }
        public string Branch { get; set; }
        public decimal? Amount { get; set; }
        public string Ifsc { get; set; }
        public int? Uid { get; set; }

        public Users U { get; set; }
    }
}
