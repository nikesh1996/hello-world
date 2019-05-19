using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infosys.BioKartDAL;
using Infosys.BioKartDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Infosys.BioKartMVC.Controllers
{
    public class AuctionController : Controller
    {
        private readonly BioKartRepository _repObj;
        private readonly IMapper _mapper;
        public AuctionController(BiokartContext context, IMapper mapper)
        {
            _repObj = new BioKartRepository(context);
            _mapper = mapper;
        }




        public IActionResult AddAuctionItem(Models.Auctionstock fsObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {

                return RedirectToAction("Login", "Home");

            }
            if (_repObj.GetRoleId(uid) != "E" || _repObj.GetRoleId(uid) != "A")
            {
                if (_repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }
                if (_repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }

            }

            Models.AuctionItem auctionitem = new Models.AuctionItem();
            auctionitem.InitialQuantity = fsObj.Quantity;
            TempData["InitialQuantity"] = fsObj.Quantity;
            TempData["PricePerUnit"] = fsObj.PricePerUnit;
            auctionitem.SellerUid = fsObj.Uid;
            auctionitem.Item = fsObj.Item;
            auctionitem.PricePerUnit = fsObj.PricePerUnit;
            auctionitem.StartDate = DateTime.Now;
            auctionitem.EmpId = uid;
            auctionitem.BasePrice = fsObj.Quantity * fsObj.PricePerUnit;
            auctionitem.FinalQuantity = fsObj.Quantity;



            return View(auctionitem);
        }
        public IActionResult SaveAddAuctionItem(Models.AuctionItem aucObj)
        {
            

            if (aucObj.BasePrice < 1)
            {
                TempData["InvalidDate"] = "Invalid Base Price! Auction Creation Failed.";
                return RedirectToAction("ViewAuctionStock", "employee");
            }
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            aucObj.EmpId = uid;
            if (aucObj.EndDate <= DateTime.Now.Date)
            {
                TempData["InvalidDate"] = "You have entered an invalid date. Auction Creation Failed.";
                return RedirectToAction("ViewAuctionStock", "Employee");
            }

            bool status = _repObj.SaveAuctionItem(_mapper.Map<AuctionItem>(aucObj));
            if (status)
            {
                TempData["SuccessAuction"] = "Auction started successfully";
                TempData["AllAuctions"] = "";
                return RedirectToAction("GetMyAuctions");
            }
            else
            {
                return View("Error");
            }


        }
        public IActionResult GetMyAuctions()
        {

            try
            {

                int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


                if (uid == 0)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (_repObj.GetRoleId(uid) != "E" || _repObj.GetRoleId(uid) != "A")
                {
                    if (_repObj.GetRoleId(uid) == "C")
                    {
                        return RedirectToAction("CustomerHome", "User");
                    }
                    if (_repObj.GetRoleId(uid) == "F")
                    {
                        return RedirectToAction("FarmerHome", "User");
                    }

                }

                List<AuctionItem> list1 = _repObj.GetAllAuctionsByEmpid(uid);
                List<Models.AuctionItem> list2 = new List<Models.AuctionItem>();
                if (list1.Count == 0)
                {
                    return View("NoAuctions");
                }
                foreach (var item in list1)
                {

                    list2.Add(_mapper.Map<Models.AuctionItem>(item));
                }
                return View(list2);
            }
            catch (Exception)
            {
                return View("Error");
            }



        }
        public IActionResult DropAuction(Models.AuctionItem obj)
        {
            
            try
            {
                int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


                if (uid == 0)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (_repObj.GetRoleId(uid) != "E" || _repObj.GetRoleId(uid) != "A")
                {
                    if (_repObj.GetRoleId(uid) == "C")
                    {
                        return RedirectToAction("CustomerHome", "User");
                    }
                    if (_repObj.GetRoleId(uid) == "F")
                    {
                        return RedirectToAction("FarmerHome", "User");
                    }

                }

                bool status = false;
                if (!_repObj.isaBiddedItem(obj.AuctionId))
                {
                    status = _repObj.DeleteAuction(_mapper.Map<AuctionItem>(obj));
                    if (status)
                    {
                        TempData["AllAuctions"] = "Auction Deleted!";

                        return RedirectToAction("GetMyAuctions");
                    }
                    else
                    {
                        TempData["AllAuctions"] = "Some Error Occured!";
                        return RedirectToAction("GetMyAuctions");

                    }


                }

                else
                {
                    TempData["AllAuctions"] = "Can't drop an already bidded item!";
                    return RedirectToAction("GetMyAuctions");


                }
            }
            catch (Exception)
            {
                return View("Error");
            }



        }
        public IActionResult NoAuctions()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

           
            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            

            return View();
        }


        public IActionResult ViewAllOnAuctions()
        {
            try
            {
                int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


                if (uid == 0)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (_repObj.GetRoleId(uid) != "C")
                {
                    if (_repObj.GetRoleId(uid) == "E")
                    {
                        return RedirectToAction("EmployeeHome", "User");
                    }
                    if (_repObj.GetRoleId(uid) == "F")
                    {
                        return RedirectToAction("FarmerHome", "User");
                    }

                }
                var list1 = _repObj.GetAllOnAuctions();
                if (list1.Count == 0)
                {
                    TempData["NoOonAuctions"] = "Sorry, there is no ongoing auction. Stay tuned!";
                    return RedirectToAction("CustomerHome","User");
                }
                List<Models.AuctionItem> l2 = new List<Models.AuctionItem>();
                foreach (var item in list1)
                {
                    l2.Add(_mapper.Map<Models.AuctionItem>(item));
                }
                return View(l2);
            }
            catch (Exception)
            {

                return View("Error");
            }

        }


        public IActionResult NoAuctionCustomer()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }






        public IActionResult AddAuctionBid(Models.AuctionItem Obj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {

                return RedirectToAction("Login", "Home");
            }
            if (_repObj.GetRoleId(uid) != "C")
            {
                if (_repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (_repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }

            }

            Models.AuctionBid auctionbid = new Models.AuctionBid();

            auctionbid.AuctionId = Obj.AuctionId;
            int a = Obj.AuctionId;
            TempData["BaseBid"] = Obj.BasePrice;
            auctionbid.BidDate = DateTime.Now;
            auctionbid.BidderId = uid;
            var query = _repObj.HighestBid((Obj.AuctionId));
            if (query == 0)
            {
                TempData["HighestBid"] = 0;//Obj.BasePrice;
            }
            else
            {
                TempData["HighestBid"] = _repObj.HighestBid((Obj.AuctionId));

            }
            ViewBag.PreviousBids = _repObj.PreviousBids(Obj.AuctionId, uid);
            ///
            if (_repObj.HighestBid(a) == 0)
            {
                TempData["OldBid"] = Obj.BasePrice;

            }
            else
            {
                TempData["OldBid"] = (double)_repObj.HighestBid(a) + 0.01;

            }


            return View(auctionbid);
        }

        public IActionResult SaveAddAuctionBid(Models.AuctionBid aucObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }

            bool status = _repObj.SaveAuctionBid(_mapper.Map<AuctionBid>(aucObj));
            if (status)
            {
                TempData["BidAdded"] = "Success! You just added a new bid!";
                return RedirectToAction("ViewAllOnAuctions");
            }
            else
            {
                return View("Error");
            }

        }



        public IActionResult GetBidDetails(Models.AuctionItem aucObj)
        {
            try
            {
                int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


                if (uid == 0)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (_repObj.GetRoleId(uid) != "E" || _repObj.GetRoleId(uid) != "A")
                {
                    if (_repObj.GetRoleId(uid) == "C")
                    {
                        return RedirectToAction("CustomerHome", "User");
                    }
                    if (_repObj.GetRoleId(uid) == "F")
                    {
                        return RedirectToAction("FarmerHome", "User");
                    }

                }

                List<AuctionBid> list1 = _repObj.HighestBidofEachBidder(aucObj.AuctionId);

                List<Models.AuctionBid> list2 = new List<Models.AuctionBid>();
                if (list1.Count == 0)
                {
                    TempData["SuccessAuction"] = "";
                    TempData["AllAuctions"] = "No bid recieved yet!";
                    return RedirectToAction("GetMyAuctions");
                }
                decimal bid = 0;
                foreach (var item in _repObj.HighestBidofEachBidder((aucObj.AuctionId)))
                {
                    bid = item.BidAmount;
                    break;
                }
                TempData["HighestBid"] = bid;
                TempData["TotalBid"] = _repObj.TotalBid((aucObj.AuctionId));
                TempData["AuctionIdforHighest"] = aucObj.AuctionId;


                foreach (var item in list1)
                {

                    list2.Add(_mapper.Map<Models.AuctionBid>(item));
                }

                return View(list2);
            }
            catch (Exception)
            {
                return View("Error");
            }



        }


        public IActionResult AddAuctionCart()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {

                return RedirectToAction("Login", "Home");
            }
            try
            {
                int aucid = (int)TempData["AuctionIdforHighest"];

                AuctionBid productObj = new AuctionBid();
                foreach (var item in _repObj.HighestBidofEachBidder((aucid)))
                {
                    productObj = item;
                    break;
                }
               
                PurchaseDetails obj = new PurchaseDetails();
                int? abc = productObj.AuctionId;
                obj.Buyer = productObj.BidderId;
                obj.TotalAmount = productObj.BidAmount;
                obj.OrderedDate = DateTime.Now;
                obj.DeliveryDate = DateTime.Now.AddDays(7);
                obj.Name = _repObj.GetCustomerName(productObj.BidderId);
                obj.Seller = _repObj.GetFarmer((int)productObj.AuctionId);
                obj.ItemName = _repObj.GetItemNameforAuction((int)productObj.AuctionId);
                obj.QuantityPurchased = _repObj.GetQuantityForAuction((int)productObj.AuctionId);
                obj.PricePerUnit = obj.TotalAmount / obj.QuantityPurchased;
                
                var status = _repObj.PurchaseProduct(obj);
                if (status)
                {
                    bool a = _repObj.SuccessBuyNotification(obj.ItemName, obj.Buyer, obj.DeliveryDate, obj.Seller, "Auction");
                    if (a)
                    {

                        PastAuctionResult part = new PastAuctionResult();
                        int aid = aucid;
                        part.AuctionId = aid; 
                        part.WinnerId = productObj.BidderId;
                        part.FarmerId = obj.Seller;
                        part.BidAmount = productObj.BidAmount;
                        part.EndDate = obj.OrderedDate;
                        _repObj.AddPastAuctionResult(part,aid);

                       _repObj.UpdateFromAuctionItemByName(obj.ItemName, part.FarmerId,aucid);

                        TempData["SuccessAuction"] = "Success, Deal Done Successfully!";
                        return RedirectToAction("GetMyAuctions", "Auction");

                    }
                    TempData["AllAuctions"] = "Something went wrong. Please re-do the auction deal.";
                    return RedirectToAction("GetMyAuctions", "Auction");
                }
                TempData["AllAuctions"] = "Something went wrong. Please re-do the auction deal.";
                return RedirectToAction("GetMyAuctions", "Auction");


            }
            catch (Exception)
            {

                return View("Error");
            }





        }
        public IActionResult MyAuctionCart()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var lstEntityProducts = _repObj.GetYourAuctionCart(cidn);
            if (lstEntityProducts.Count == 0)
            {
                TempData["NoAuctionCart"] = "No Auctions Won yet! Stay tuned! Now viewing your regular cart!";
                return RedirectToAction("YourCart", "Customer");
            }
            List<Models.AuctionCart> lstModelProducts = new List<Models.AuctionCart>();

            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.AuctionCart>(product));
            }

            return View(lstModelProducts);
        }



        public IActionResult PurchaseAuctionProduct(Models.AuctionCart productObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            Models.PurchaseDetails purchase = new Models.PurchaseDetails();
            purchase.Buyer = (int)productObj.Buyer;
            purchase.Seller = (int)productObj.Seller;
            purchase.Name = _repObj.GetCustomerName(purchase.Buyer);
            purchase.OrderedDate = DateTime.Now;
            purchase.ItemName = productObj.ItemName;
            purchase.QuantityPurchased = _repObj.GetQuantityForAuction(productObj.AuctionId);
            purchase.PricePerUnit = productObj.TotalPrice / purchase.QuantityPurchased;
            purchase.TotalAmount = productObj.TotalPrice;
            TempData["AuctionIdforPurchaseinCustomer"] = productObj.AuctionId;


            return View(purchase);
        }



        public IActionResult Pastauction()
        {


            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            var lstPastAuction = _repObj.Pastauction();
            if (lstPastAuction.Count == 0)
            {
                TempData["NoPastAuction"] = "No Past Auction";
                return RedirectToAction("GetMyAuctions", "Auction");
            }
            List<Models.PastAuctionResult> lstModelProducts = new List<Models.PastAuctionResult>();

            foreach (var product in lstPastAuction)
            {
                lstModelProducts.Add(_mapper.Map<Models.PastAuctionResult>(product));
            }

            return View(lstModelProducts);
        }


        //public ActionResult SaveAuctionPurchase(Models.PurchaseDetails purchaseObj)
        //{
        //    int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


        //    if (uid == 0)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
        //            if (purchaseObj.DeliveryDate <= purchaseObj.OrderedDate)
        //            {
        //                //change here
        //                return View("Error");
        //            }

        //            purchaseObj.DeliveryDate = (DateTime.Now).AddDays(7.0);


        //            var status = _repObj.PurchaseProduct(_mapper.Map<PurchaseDetails>(purchaseObj));
        //            if (status)
        //            {
        //                bool a = _repObj.SuccessBuyNotification(purchaseObj.ItemName, cidn, purchaseObj.DeliveryDate, purchaseObj.Seller);
        //                if (a)
        //                {
        //                    int aid = (int)TempData["AuctionIdforPurchaseinCustomer"];
        //                    //_repObj.DeleteFromAuctionCartByName(purchaseObj.ItemName, cidn, aid);
        //                    PastAuctionResult part = new PastAuctionResult();
        //                    part.AuctionId = aid;
        //                    part.WinnerId = purchaseObj.Buyer;
        //                    part.FarmerId = purchaseObj.Seller;
        //                    part.BidAmount = purchaseObj.TotalAmount;
        //                    part.EndDate = purchaseObj.OrderedDate;
        //                    _repObj.AddPastAuctionResult(part,aid);
        //                }

        //                return View("SuccessPurchase");
        //            }

        //            else
        //                return View("Error");


        //        }
        //        catch (Exception)
        //        {
        //            return View("Error");
        //        }
        //    }
        //    return View("PurchaseProduct", purchaseObj);
        //}

        //    public IActionResult AddToPurchase(List<Models.AuctionBid> product)
        //    {
        //        int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));
        //        Models.AuctionBid productObj = new Models.AuctionBid();
        //        if (uid == 0)
        //        {
        //            return RedirectToAction("Login", "Home");
        //        }
        //        foreach (var item in product)
        //        {
        //            productObj = item;
        //            break;
        //        }
        //        PurchaseDetails obj = new PurchaseDetails();
        //        obj.Buyer = productObj.BidderId;
        //        obj.TotalAmount = productObj.BidAmount;
        //        obj.OrderedDate = DateTime.Now;
        //        obj.DeliveryDate = DateTime.Now.AddDays(7);
        //        obj.Name = _repObj.GetCustomerName(productObj.BidderId);
        //        obj.Seller = _repObj.GetFarmer(productObj.AuctionId);
        //        obj.ItemName = _repObj.GetItemNameforAuction(productObj.AuctionId);
        //        obj.QuantityPurchased = _repObj.GetQuantityForAuction(productObj.AuctionId);
        //        obj.PricePerUnit = obj.TotalAmount / obj.QuantityPurchased;
        //        var status = _repObj.PurchaseProduct(obj);
        //        //farmer
        //        if (status)
        //        {
        //            bool a = _repObj.SuccessBuyNotification(obj.ItemName, obj.Buyer, obj.DeliveryDate, obj.Seller);
        //            if (a)
        //            {

        //                PastAuctionResult part = new PastAuctionResult();
        //                part.AuctionId = productObj.AuctionId;
        //                part.WinnerId = productObj.BidderId;
        //                part.FarmerId = obj.Seller;
        //                part.BidAmount = productObj.BidAmount;
        //                part.EndDate = obj.OrderedDate;
        //                _repObj.AddPastAuctionResult(part);
        //                TempData["SuccessAuction"] = "Success, Deal Done Successfully!";
        //                return RedirectToAction("GetMyAuctions", "Auction");
        //            }
        //            TempData["AllAuctions"] = "Something went wrong. Please re-do the auction deal.";
        //            return RedirectToAction("GetMyAuctions", "Auction");
        //        }
        //        TempData["AllAuctions"] = "Something went wrong. Please re-do the auction deal.";
        //        return RedirectToAction("GetMyAuctions", "Auction");

        //    }
        //}
    }
}


