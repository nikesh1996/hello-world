using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Auctionstock = new HashSet<Auctionstock>();
            FarmerStock = new HashSet<FarmerStock>();
            ItemSellable = new HashSet<ItemSellable>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Auctionstock> Auctionstock { get; set; }
        public ICollection<FarmerStock> FarmerStock { get; set; }
        public ICollection<ItemSellable> ItemSellable { get; set; }
    }
}
