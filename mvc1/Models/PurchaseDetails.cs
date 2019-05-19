using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infosys.BioKartMVC.Models
{
    public class PurchaseDetails
    {
        public int Buyer { get; set; }
        public string Name { get; set; }
        public int Seller { get; set; }
        public char PurchaseType { get; set; }
        public int PurchaseId { get; set; }
        public string ItemName { get; set; }
        [DisplayName("Quantity purchased")]
        [Required(ErrorMessage = "Quantity purchased is required")]
        public int QuantityPurchased { get; set; }
        [DisplayName("Date of purchase")]
        [DisplayFormat(DataFormatString = "{dd-MM-yy}")]
        public DateTime? OrderedDate { get; set; }
        [DisplayName("Date of Delivery")]
        [DisplayFormat(DataFormatString = "{dd-MM-yy}")]
        public DateTime? DeliveryDate { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalAmount { get; set; }


    }
}
