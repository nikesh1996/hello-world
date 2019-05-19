using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infosys.BioKartDAL;
using Infosys.BioKartDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Infosys.BioKartMVC.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class FarmerController : Controller
    {
        BioKartRepository repObj;
        private readonly IMapper _mapper;
        static string userpassword3;
        public FarmerController(BiokartContext context, IMapper mapper)
        {

            repObj = new BioKartRepository(context);
            _mapper = mapper;
        }


        public IActionResult ViewStock()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var lstEntityProducts = repObj.GetFarmerStock(cidn);
            if(lstEntityProducts.Count==0)
            {
                TempData["NoRequestsForFarmer"] = "No Stock Available! Please Add a Product";
                return RedirectToAction("FarmerHome","User");
            }
            List<Models.FarmerStock> lstModelProducts = new List<Models.FarmerStock>();
            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.FarmerStock>(product));
            }
            return View(lstModelProducts);
        }

        public IActionResult ViewAuctionStock()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var lstEntityProducts = repObj.GetAuctionStock(cidn);
            if(lstEntityProducts.Count==0)
            {
                return View("NoAuctions");
            }
            List<Models.Auctionstock> lstModelProducts = new List<Models.Auctionstock>();
            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.Auctionstock>(product));
            }
            return View(lstModelProducts);
        }

        public IActionResult GetTopBuyers()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            var lstEntityProducts = repObj.Topbuyers();
            List<Models.PurchaseDetails> lstModelProducts = new List<Models.PurchaseDetails>();
            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.PurchaseDetails>(product));
            }
            return View(lstModelProducts);
        }

        public IActionResult FarmDetails(Models.FarmerStock farmer)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            Users obj = repObj.GetUserDetails(farmer.Uid);
            Models.FarmerUser up = _mapper.Map<Models.FarmerUser>(obj);

            return View(up);
        }


        public IActionResult AddAuctionProduct()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View();
        }

        public IActionResult SaveAddedAuctionProduct(Models.Auctionstock product)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            bool status = false;
            bool containsInt = product.Item.Any(char.IsDigit);
            if(product.CategoryId==null)
            {
                TempData["AddProductFarmerAuction"] = "Please Select a Category!";
                return RedirectToAction("AddAuctionProduct", "farmer");
            }
            if (containsInt)
            {
                TempData["AddProductFarmerAuction"] = "Invalid Item Name!";
                return RedirectToAction("AddAuctionProduct", "farmer");
            }
            if (product.PricePerUnit < 1)
            {
                TempData["AddProductFarmerAuction"] = "Invalid Price!";
                return RedirectToAction("AddAuctionProduct", "farmer");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));

                    status = repObj.AddAuctionProduct(_mapper.Map<Infosys.BioKartDAL.Models.Auctionstock>(product), cidn);
                    if (status)
                        return RedirectToAction("ViewAuctionStock");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("AddAuctionProduct", product);
        }



        public IActionResult AddProduct()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View();
        }

        [HttpPost]
        public IActionResult SaveAddedProduct(Models.FarmerStock product)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            bool containsInt = product.Item.Any(char.IsDigit);
            if (product.CategoryId == null)
            {
                TempData["AddProductFarmer"] = "Please Select a Category!";
                return RedirectToAction("AddProduct", "farmer");
            }

            if (containsInt)
            {
                TempData["AddProductFarmer"] = "Invalid Item Name!";
                return RedirectToAction("AddProduct", "farmer");
            }
            bool status = false;
            if (product.PricePerUnit<1)
            {
                TempData["AddProductFarmer"] = "Invalid Price!";
                return RedirectToAction("AddProduct", "farmer");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                  
                    status = repObj.AddProduct(_mapper.Map<Infosys.BioKartDAL.Models.FarmerStock>(product),cidn);
                    if (status)
                        return RedirectToAction("ViewStock");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("AddProduct", product);
        }



        public IActionResult UpdateAuctionProduct(Models.Auctionstock prodObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View(prodObj);
        }

        public IActionResult SaveUpdatedAuctionProduct(Models.Auctionstock product)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            if (product.PricePerUnit < 1)
            {
                TempData["UpdateProductFarmerAuction"] = "Invalid Price! Minimum Price is ₹ 1.00. Updation Failed.";
                return RedirectToAction("ViewAuctionStock", "farmer");
            }
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                    status = repObj.UpdateAuctionProduct(_mapper.Map<Infosys.BioKartDAL.Models.Auctionstock>(product), cidn);
                    if (status)
                        return RedirectToAction("ViewAuctionStock");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("UpdateAuctionProduct", product);
        }




        public IActionResult UpdateProduct(Models.FarmerStock prodObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View(prodObj);
        }


        [HttpPost]
        public IActionResult SaveUpdatedProduct(Models.FarmerStock product)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            if (product.PricePerUnit<1)
            {
                TempData["-veprice"] = "Invalid Price! Updation failed.";
                return RedirectToAction("ViewStock","Farmer");
            }
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                   
                   
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                    status = repObj.UpdateProduct(_mapper.Map<Infosys.BioKartDAL.Models.FarmerStock>(product),cidn);
                    if (status)
                        return RedirectToAction("ViewStock");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("UpdateProduct", product);
        }

        public IActionResult DeleteAuctionstock(Models.Auctionstock userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View(userObj);
        }


        public IActionResult SaveDeletionAuctionStock(Models.Auctionstock Obj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            bool status = false;
            try
            {
                int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                status = repObj.DeleteAuctionStock(cidn, Obj.Item);
                if (status)
                    return RedirectToAction("ViewAuctionStock");
                else
                    return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public IActionResult Deletestock(Models.FarmerStock userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View(userObj);
        }

        public IActionResult SaveDeletionStock(Models.FarmerStock Obj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            bool status = false;
            try
            {
                int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                status = repObj.DeleteStock(cidn, Obj.Item);
                if (status)
                    return RedirectToAction("ViewStock");
                else
                    return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public IActionResult MyDetails()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var empObj = repObj.GetCustomerById(cidn);
            userpassword3 = empObj.UserPassword;
            Models.FarmerEmployeeUserD obj = _mapper.Map<Models.FarmerEmployeeUserD>(empObj);
            ViewBag.MyEarnings = repObj.getmykami(cidn);
            return View(obj);
        }
        public IActionResult SaveMyUpdatedDetail(Models.FarmerEmployeeUserD userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }

            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    userObj.UserPassword = userpassword3;
                     status = repObj.UpdateFarmerDetails(_mapper.Map<Users>(userObj));
                    HttpContext.Session.SetString("username", userObj.Name.Split()[0]);
                    HttpContext.Session.SetString("uid", Convert.ToString(userObj.Uid));
                    if (status)
                        return RedirectToAction("MyDetails");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("Update", userObj);
        }
        public IActionResult UpdateMyDetails(Models.FarmerEmployeeUserD userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View(userObj);
        }
        public IActionResult this1()
        {
            return View("Error");
        }
        public IActionResult CustomerRequests(Models.AdminForwardedRequests req)
        {

            try
            {
                int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

                if (uid == 0)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (repObj.GetRoleId(uid) != "F")
                {
                    if (repObj.GetRoleId(uid) == "E")
                    {
                        return RedirectToAction("EmployeeHome", "User");
                    }
                    if (repObj.GetRoleId(uid) == "C")
                    {
                        return RedirectToAction("CustomerHome", "User");
                    }

                }
                var query = repObj.GetAllRequests();
                if(query.Count==0)
                {
                    TempData["NoRequestsForFarmer"] = "No Requests Available";
                    return RedirectToAction("FarmerHome","User");
                }
                List<Models.AdminForwardedRequests> list1 = new List<Models.AdminForwardedRequests>();
                foreach (var item in query)
                {
                    list1.Add(_mapper.Map<Models.AdminForwardedRequests>(item));
                }
                return View(list1);
            }
            catch (Exception)
            {

                return View("Error");
            }
          
        }
        public IActionResult CommitView(Models.AdminForwardedRequests obj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            Models.FarmerStock obj1 = new Models.FarmerStock();
            obj1.Item = obj.Item;
            obj1.Quantity = obj.Quantity;
            obj1.Uid = cidn;
            TempData["Arid"] = obj.Arid;
            return View(obj1);
        }
        public IActionResult Commit(Models.FarmerStock obj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            bool status = false;
           
                try
                {
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                    if(obj.PricePerUnit<1)
                {
                    TempData["InvalidRashi"] = "Invalid Price, Set unit price atleast ₹ 1.00";
                    return RedirectToAction("CustomerRequests", "Farmer");
                }

                    status = repObj.AddProduct(_mapper.Map<Infosys.BioKartDAL.Models.FarmerStock>(obj), cidn);
                    int? arid = TempData["Arid"] as int?;
                    if (status)
                    {
                        bool ij = repObj.CloseRequest(arid,cidn);

                        if (ij)
                        {
                            int cid = repObj.GetCustomerID(arid);
                            int? rid = repObj.GetCustomerRID(arid);
                            bool s = repObj.PushSuccessNotification(rid, cid, cidn);
                            if (s)
                            {
                                return RedirectToAction("ViewStock", "Farmer");
                            }
                            else
                                return View("Error");
                        }
                        return View("Error");

                    }
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            
          
        }
        public IActionResult GetNotifications()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var query = repObj.GetNotificationsF(cidn);
            List<Models.Notifications> nList = new List<Models.Notifications>();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    nList.Add(_mapper.Map<Models.Notifications>(item));
                }
                return View(nList);
            }
            return RedirectToAction("NoNotification");

        }
        public IActionResult NoNotification()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View();

        }
      
        public IActionResult DeleteNotification(Models.Notifications obj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            int nid = obj.Nid;
            bool query = repObj.DeleteNotification(nid);
            if (query)
            {
                return RedirectToAction("GetNotifications", "Farmer");
            }
            return View("Error");
        }
        public IActionResult DeleteAllNotification()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            bool query = repObj.DeleteAllNotification(cidn);
            if (query)
            {
                return RedirectToAction("GetNotifications", "Farmer");
            }
            return View("Error");
        }
        public IActionResult NoAuctions()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View();
        }
        public IActionResult ResetPass()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            return View();
        }
        public IActionResult SaveResetPass(Models.ResetPasswordDashboard obj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "F")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }

            }
            Users obj1 = repObj.GetUserDetails(uid);
            string email = obj1.EmailId;
            if (obj1.UserPassword != obj.oldpassword)
            {
                TempData["OhBhaiji1"] = "Old Password is Wrong! Please Check";
                return RedirectToAction("ResetPass", "Farmer");
            }
            if (obj.newPassword != obj.confirmpassword)
            {
                TempData["OhBhaiji1"] = "New Password and Confirm Password do not match! Please Check";
                return RedirectToAction("ResetPass", "Farmer");
            }
            if (obj.newPassword == obj.confirmpassword && obj1.UserPassword == obj.newPassword)
            {
                TempData["OhBhaiji1"] = "New Password cannot be same as old.";
                return RedirectToAction("ResetPass", "Farmer");
            }
            var status = repObj.ResetPassword(email, obj.newPassword, obj.confirmpassword);
            if (status)
            {
                TempData["BhaijiReturns1"] = "Password Updated Successful!";
                return RedirectToAction("MyDetails", "Farmer");
            }
            else
            {
                TempData["BhaijiReturns1"] = "Password update failed!";
                return RedirectToAction("MyDetails", "Farmer");
            }

        }

    }
}