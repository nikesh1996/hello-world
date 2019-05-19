using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Infosys.BioKartMVC.Models
{
    public class ResetPassword
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "EmailId is mandatory.")]
        [DisplayName("Email Id")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Birth Place is mandatory.")]
        public string Birthplace { get; set; }
    }
}
