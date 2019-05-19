using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;



namespace Infosys.BioKartMVC.Models
{
    public class ConfirmPassword
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "EmailId is mandatory.")]
        [DisplayName("Email Id")]
        public string EmailId { get; set; }

        [StringLength(maximumLength: 10)]
        [Required(ErrorMessage = "Password is mandatory.")]
        [DisplayName("User password")]
        public string UserPassword { get; set; }

        [StringLength(maximumLength: 10)]
        [Required(ErrorMessage = "Password is mandatory.")]
        [DisplayName("User password")]
        public string UserPassword1 { get; set; }


    }
}
