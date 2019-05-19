using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class Notifications
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public int? FarmerId { get; set; }
        public int Nid { get; set; }
        public DateTime? Created { get; set; }
    }
}
