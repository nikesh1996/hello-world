using System;
using System.Collections.Generic;

namespace Infosys.BioKartDAL.Models
{
    public partial class Users
    {
        public Users()
        {
            AdminForwardedRequest = new HashSet<AdminForwardedRequest>();
            AuctionBid = new HashSet<AuctionBid>();
            AuctionItemEmp = new HashSet<AuctionItem>();
            AuctionItemSellerU = new HashSet<AuctionItem>();
            Auctionstock = new HashSet<Auctionstock>();
            FarmBank = new HashSet<FarmBank>();
            FarmerStock = new HashSet<FarmerStock>();
            PastAuctionResultFarmer = new HashSet<PastAuctionResult>();
            PastAuctionResultWinner = new HashSet<PastAuctionResult>();
            PurchaseDetails = new HashSet<PurchaseDetails>();
            Requests = new HashSet<Requests>();
        }

        public string Name { get; set; }
        public int Uid { get; set; }
        public string EmailId { get; set; }
        public string UserPassword { get; set; }
        public string RoleId { get; set; }
        public string Pan { get; set; }
        public long PhoneNo { get; set; }
        public string Address { get; set; }
        public string Security { get; set; }
        public string Pin { get; set; }

        public Roles Role { get; set; }
        public ICollection<AdminForwardedRequest> AdminForwardedRequest { get; set; }
        public ICollection<AuctionBid> AuctionBid { get; set; }
        public ICollection<AuctionItem> AuctionItemEmp { get; set; }
        public ICollection<AuctionItem> AuctionItemSellerU { get; set; }
        public ICollection<Auctionstock> Auctionstock { get; set; }
        public ICollection<FarmBank> FarmBank { get; set; }
        public ICollection<FarmerStock> FarmerStock { get; set; }
        public ICollection<PastAuctionResult> PastAuctionResultFarmer { get; set; }
        public ICollection<PastAuctionResult> PastAuctionResultWinner { get; set; }
        public ICollection<PurchaseDetails> PurchaseDetails { get; set; }
        public ICollection<Requests> Requests { get; set; }
    }
}
