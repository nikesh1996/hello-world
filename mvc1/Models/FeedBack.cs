using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class FeedBack
    {


       
        public string Name { get; set; }
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "EmailId is mandatory.")]
        public string EmailId { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
    }

}
