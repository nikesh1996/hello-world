using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class PurchaseDetails
    {
        public int Buyer { get; set; }
        public int Seller { get; set; }
        public string Name { get; set; }
        public string PurchaseType { get; set; }
        public int PurchaseId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? OrderedDate { get; set; }
        public string ItemName { get; set; }
        public int QuantityPurchased { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalAmount { get; set; }

        public Users BuyerNavigation { get; set; }
    }
}
