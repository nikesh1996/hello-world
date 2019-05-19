using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Infosys.BioKartMVC.Models
{
    public class FarmerUser
    {
        [Required(ErrorMessage = "Name is mandatory.")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "EmailId is mandatory.")]
        [DisplayName("Email Id")]
        public string EmailId { get; set; }

        [StringLength(maximumLength: 10)]
        [Required(ErrorMessage = "Password is mandatory.")]
        [DisplayName("User password")]
        public string UserPassword { get; set; }
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Birth Place is mandatory.")]
        public string Birthplace { get; set; }

        [Required(ErrorMessage = "PAN Number is mandatory.")]
        public string PAN { get; set; }

        [Required(ErrorMessage = "Phone Number is mandatory.")]
        [DisplayName("Phone Number")]
        [RegularExpression(@"^(\d{10})$")]
        public long Phno { get; set; }

        [Required(ErrorMessage = "PIN Code is mandatory.")]
        [DisplayName("PIN Code")]
        [RegularExpression(@"^(\d{6})$")]
        public string Pincode { get; set; }


        [Required(ErrorMessage = "Address is mandatory.")]
        public string Address { get; set; }


        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string RoleId { get; set; }

    }
}




