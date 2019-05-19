using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Infosys.BioKartMVC.Models
{
    public class CustomerUser
    {
        [Required(ErrorMessage = "Name is mandatory.")]
        [DisplayName("Name")]
        public string Name { get; set; }

        //[RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
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

        [RegularExpression(@"^(\d{6})$")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Phone Number is mandatory.")]
        [DisplayName("Phone Number")]
        [RegularExpression(@"^(\d{10})$")]
        public long Phno { get; set; }

        [Required(ErrorMessage = "Address is mandatory.")]
        public string Address { get; set; }



        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public string RoleId { get; set; }

        [ScaffoldColumn(false)]
        public string PAN { get; set; }

    }
}




