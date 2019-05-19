using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class Notifications
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public int? FarmerId { get; set; }
        public int Nid { get; set; }
        public DateTime? Created { get; set; }
    }
}
