using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class ItemSellable
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Categories Category { get; set; }
    }
}
