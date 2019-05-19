using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Infosys.BioKartDAL;
using Infosys.BioKartDAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Infosys.BioKartMVC.Controllers
{
    
    //[Route("api/[controller]")]
    //[ApiController]
    public class CustomerController : Controller
    {
        static int? categoryidforuse=100;
        static string userpassword11;
        BioKartRepository repObj;
        private readonly IMapper _mapper;

        public CustomerController(BiokartContext context, IMapper mapper)
        {

            repObj = new BioKartRepository(context);
            _mapper = mapper;
        }

        public IActionResult C_ViewProducts()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }

            var lstEntityProducts = repObj.GetWareHouseStock();
            List<Models.FarmerStock> lstModelProducts = new List<Models.FarmerStock>();
            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.FarmerStock>(product));
            }
            return View(lstModelProducts);
        }

        public IActionResult SortResult()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            //int? category = (int)TempData["Sortbythiscategory"];
            if(categoryidforuse!=100)
            {
                if (categoryidforuse== null)
                {
                    var lstEntityProducts2 = repObj.NearestFarmerForAll(cidn);
                    List<Models.FarmerStock> lstModelProducts2 = new List<Models.FarmerStock>();
                    foreach (var product in lstEntityProducts2)
                    {
                        lstModelProducts2.Add(_mapper.Map<Models.FarmerStock>(product));
                    }
                    TempData["ReturnToMain"] = null;

                    categoryidforuse = 100;
                    return View(lstModelProducts2);
                }
                else
                {
                    var lstEntityProducts2 = repObj.NearestFarmer(cidn, categoryidforuse);
                    List<Models.FarmerStock> lstModelProducts2 = new List<Models.FarmerStock>();
                    foreach (var product in lstEntityProducts2)
                    {
                        lstModelProducts2.Add(_mapper.Map<Models.FarmerStock>(product));
                    }
                    categoryidforuse = 100;
                    TempData["ReturnToMain"] = (int?)TempData["Sortbythiscategory"];
                    return View(lstModelProducts2);
                }

            }
            if(TempData["Sortbythiscategory"]==null )
            {
                var lstEntityProducts2 = repObj.NearestFarmerForAll(cidn);
                List<Models.FarmerStock> lstModelProducts2 = new List<Models.FarmerStock>();
                foreach (var product in lstEntityProducts2)
                {
                    lstModelProducts2.Add(_mapper.Map<Models.FarmerStock>(product));
                }
                TempData["ReturnToMain"] = null;
                categoryidforuse = 100;
                return View(lstModelProducts2);
            }
            var lstEntityProducts = repObj.NearestFarmer(cidn, (int?)TempData["Sortbythiscategory"]);
            List<Models.FarmerStock> lstModelProducts = new List<Models.FarmerStock>();
            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.FarmerStock>(product));
            }
            TempData["ReturnToMain"] = (int?)TempData["Sortbythiscategory"];
            return View(lstModelProducts);
        }


        public IActionResult FarmDetails2(Models.FarmerStock farmer)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            Users obj = repObj.GetUserDetails(farmer.Uid);
            Models.FarmerDetails up = _mapper.Map<Models.FarmerDetails>(obj);

            return View(up);
        }
        public IActionResult FarmDetails3(Models.FarmerStock farmer)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            Users obj = repObj.GetUserDetails(farmer.Uid);
            Models.FarmerDetails up = _mapper.Map<Models.FarmerDetails>(obj);

            return View(up);
        }
       
        public IActionResult FarmDetails4(Models.FarmerStock farmer)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            Users obj = repObj.GetUserDetails(farmer.Uid);
            Models.FarmerDetails up = _mapper.Map<Models.FarmerDetails>(obj);

            return View(up);
        }




        public IActionResult ViewCart(Models.FarmerStock userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            return View(userObj);
        }

        [HttpPost]
        public IActionResult SaveAddedCart(Models.FarmerStock product)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    decimal price = product.PricePerUnit;
                    product.PricePerUnit = price;
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                    status = repObj.AddCart(_mapper.Map<FarmerStock>(product), cidn);
                    if (status)
                        return RedirectToAction("C_ViewProducts");
                    else
                        return View("Error");
                }

                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("AddCart", product);

        }
        public IActionResult SaveAddedCart1(Models.FarmerStock product)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    decimal price = product.PricePerUnit;
                    product.PricePerUnit = price;
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                    status = repObj.AddCart(_mapper.Map<FarmerStock>(product), cidn);
                    if (status)
                        return RedirectToAction("GoBackRedirect");
                    else
                    {
                        TempData["CartFailure"] = "Product not added to cart. Please retry.";
                        return RedirectToAction("GetProductForCategory");

                    }
                }

                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("AddCart", product);

        }
        public IActionResult EmptyCart()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            return View();
        }


        public IActionResult YourCart()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var lstEntityProducts = repObj.GetYourCart(cidn);

            List<Models.Cart> lstModelProducts = new List<Models.Cart>();

            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.Cart>(product));
            }
            if (lstModelProducts.Count == 0)
                return View("EmptyCart");
            else
                return View(lstModelProducts);
        }
        public IActionResult MyPurchases()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var lstEntityProducts = repObj.GetPurchaseDetails(cidn);

            List<Models.PurchaseDetails> lstModelProducts = new List<Models.PurchaseDetails>();

            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.PurchaseDetails>(product));
            }
            if (lstModelProducts.Count == 0)
                return View("NoPurchases");
            else
                return View(lstModelProducts);
        }
        
        public IActionResult NoPurchases()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            return View();
        }
        public IActionResult DeleteCart(Models.Cart userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            return View(userObj);
        }


        public IActionResult SaveDeletionCart(Cart userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            bool status = false;
            try
            {
                status = repObj.DeleteFromCart(userObj, cidn);
                if (status)
                    return RedirectToAction("YourCart");
                else
                    return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public IActionResult ViewRequest(Models.Request req)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var lstEntityProducts = repObj.GetRequests(cidn);
           
            
           
                List<Models.Request> lstModelProducts = new List<Models.Request>();
                foreach (var product in lstEntityProducts)
                {
                    bool processed = repObj.CheckIfProcessed(product.Rid);
                    if(processed==false)
                    lstModelProducts.Add(_mapper.Map<Models.Request>(product));
                }
            if (lstModelProducts.Count != 0)
            {
                return View(lstModelProducts);
            }
            return RedirectToAction("NoRequest");
        }

        public IActionResult CustomerRequest()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            return View();

        }

        public IActionResult DeleteRequest(Models.Request userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            return View(userObj);
        }


        public IActionResult SaveDeletionRequest(Models.Request userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            if (userObj.ForwardStatus == "Y")
            {
                TempData["AckRequest"] = " Please note you cannot delete acknowleged requests!";
                return RedirectToAction("ViewRequest");
            }
            int rid = userObj.RId;
            bool status = false;
            try
            {
                status = repObj.DeleteRequest(rid);
                if (status)
                    return RedirectToAction("ViewRequest");
                else
                    return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public IActionResult deliveryinfo(Models.PurchaseDetails obj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            ViewBag.MyOrderDate = obj.OrderedDate;
            if(obj.DeliveryDate==null)
            {
                ViewBag.MyDeliveryDate = "NOT AVAILABLE";

            }
            else
            ViewBag.MyDeliveryDate = obj.DeliveryDate;
            return View(obj);
        }

        public IActionResult SaveCustomerRequest(Models.Request feed)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {

                    bool containsInt = feed.Item.Any(char.IsDigit);
                            if (containsInt)
                            {
                                TempData["InvalidRequest"] = "Invalid Item Name!";
                                return RedirectToAction("CustomerRequest", "Customer");
                            }
                 

                    {

                    }
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));

                    status = repObj.CollectRequest(_mapper.Map<Requests>(feed), cidn);
                    if (status)
                        return RedirectToAction("ViewRequest");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("ViewRequest", feed);
        }


        public IActionResult NoRequest()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            return View();
        }


        public IActionResult UpdateRequest(Models.Request userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            if (userObj.ForwardStatus == "Y")
            {
                TempData["AckRequest"] = " Please note you cannot alter acknowleged requests!";
                return RedirectToAction("ViewRequest");
            }
            return View(userObj);
        }



        public IActionResult SaveUpdatedRequest(Models.Request userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {

                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                    status = repObj.UpdateRequestDetails(_mapper.Map<Requests>(userObj), cidn);
                    if (status)
                        return RedirectToAction("ViewRequest");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("UpdateRequest", userObj);
        }


        public IActionResult GetProductForCategory(byte? categoryId)
        {
            
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }

            TempData["Sortbythiscategory"] = categoryId ;
            ViewBag.CategoryList = repObj.GetCategories();
            var productList = repObj.GetWareHouseStock();
            var products = new List<Models.FarmerStock>();
            foreach (var product in productList)
            {
                products.Add(_mapper.Map<Models.FarmerStock>(product));
            }
            if (categoryId==null)
            {
                var filteredProducts2 = products;
                return View(filteredProducts2);
            }
                var filteredProducts = products.Where(model => model.CategoryId == categoryId);
            
            return View(filteredProducts);
        }
        public IActionResult MyDetails()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var empObj = repObj.GetCustomerById(cidn);
            userpassword11 = empObj.UserPassword;
            Models.CustomerEmployeeUserD obj = _mapper.Map<Models.CustomerEmployeeUserD>(empObj);
            
            return View(obj);
        }
        public IActionResult SaveMyUpdatedDetail(Models.CustomerEmployeeUserD userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            bool status = false;
            if (ModelState.IsValid)
            {
                try
                {
                    userObj.UserPassword = userpassword11;
                    status = repObj.UpdateEmployeeDetails(_mapper.Map<Users>(userObj));
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
        public IActionResult UpdateMyDetails(Models.CustomerEmployeeUserD userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }

            }
            return View(userObj);
        }
        public IActionResult this1()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            return View("Error");
        }
        public IActionResult GetNotifications()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var query = repObj.GetNotifications(cidn);
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
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
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
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            int nid = obj.Nid;
            bool query = repObj.DeleteNotification(nid);
            if(query)
            {
                return RedirectToAction("GetNotifications", "customer");
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
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }

            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            
            bool query = repObj.DeleteAllNotification(cidn);
            if (query)
            {
                return RedirectToAction("GetNotifications", "customer");
            }
            return View("Error");
        }
        public IActionResult PurchaseProduct(Models.Cart productObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            int sellerid = productObj.Seller;
            string item = productObj.ItemName;
            int quantityinstock = repObj.GetStock(sellerid, item);
            TempData["QuantityinStock"] = quantityinstock;
            DateTime A = DateTime.Today.AddDays(1);
            string a = A.ToString("yyyy/M/dd ");
            TempData["DateDelivery"] = a;
            Models.PurchaseDetails purchaseObj = new Models.PurchaseDetails();
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            string name = repObj.GetCustomerName(cidn);
            ViewBag.Name = name.ToUpper();
            purchaseObj.Buyer = productObj.Buyer;
            purchaseObj.Seller = productObj.Seller;
            purchaseObj.PricePerUnit = productObj.PricePerUnit;
            purchaseObj.OrderedDate = DateTime.Now;
            TempData["ItemName"] = productObj.ItemName;
            
            return View(purchaseObj);
        }


        public ActionResult SavePurchase(Models.PurchaseDetails purchaseObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
                    //viewbagerrorfordate
                    if (purchaseObj.DeliveryDate<=purchaseObj.OrderedDate)
                        {
                            TempData["NoAuctionCart"]="You added a wrong delivery date. Please set it aleast one day beyond today!";
                            return RedirectToAction("YourCart","Customer");
                        }
                        ViewData["QuantityPurchased"] = purchaseObj.QuantityPurchased;
                        if (purchaseObj.QuantityPurchased >= 50)
                        {
                            purchaseObj.PurchaseType = 'B';
                            ViewData["DeliveryDate"] = purchaseObj.DeliveryDate;

                        }
                        else
                        {
                            purchaseObj.PurchaseType = 'R';
                            purchaseObj.DeliveryDate = (DateTime.Now).AddDays(7.0);

                        }
                        purchaseObj.TotalAmount = purchaseObj.QuantityPurchased * purchaseObj.PricePerUnit;

                        var status = repObj.PurchaseProduct(_mapper.Map<PurchaseDetails>(purchaseObj));
                        if (status)
                        {
                        bool a=repObj.SuccessBuyNotification(purchaseObj.ItemName,cidn,purchaseObj.DeliveryDate,purchaseObj.Seller);
                        if(a)
                        {
                            //delete from cart
                            repObj.DeleteFromCartByName(purchaseObj.ItemName, cidn);
                        }
                        //now reduce stock
                        repObj.SoldStock(purchaseObj.Seller, purchaseObj.ItemName,purchaseObj.QuantityPurchased);
                        return View("SuccessPurchase");
                        }
                          
                        else
                            return View("Error");
                  
                   
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("PurchaseProduct", purchaseObj);
        }
        public IActionResult GoBack()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            if (TempData["Sortbythiscategory"]==null)
            {
                return RedirectToAction("GetProductForCategory", "Customer");
            }
            else
            {
                return Redirect("/customer/GetProductForCategory?categoryId="+(int)TempData["Sortbythiscategory"]);
            }
                
        }




        public IActionResult GoBackRedirect()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            if (TempData["Sortbythiscategory"] == null)
            {
                return RedirectToAction("GetProductForCategory", "Customer");
            }
            else
            {
                return Redirect("/customer/GetProductForCategory?categoryId=" + (int)TempData["Sortbythiscategory"]);
            }

        }



        public IActionResult GoBack2()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            if (TempData["ReturnToMain"] == null)
            {
                categoryidforuse = null;
                return RedirectToAction("SortResult", "Customer");
            }
            else
            {
                categoryidforuse = (int)TempData["ReturnToMain"];
                return RedirectToAction("SortResult","customer");
            }

        }




        public IActionResult ResetPass()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
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
            if (repObj.GetRoleId(uid) != "C")
            {
                if (repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
            }
            Users obj1 = repObj.GetUserDetails(uid);
            string email = obj1.EmailId;
            if (obj1.UserPassword != obj.oldpassword)
            {
                TempData["OhBhaiji2"] = "Old Password is Wrong! Please Check";
                return RedirectToAction("ResetPass", "Customer");
            }
            if (obj.newPassword != obj.confirmpassword)
            {
                TempData["OhBhaiji2"] = "New Password and Confirm Password do not match! Please Check";
                return RedirectToAction("ResetPass", "Customer");
            }
            if (obj.newPassword == obj.confirmpassword && obj1.UserPassword == obj.newPassword)
            {
                TempData["OhBhaiji2"] = "New Password cannot be same as old.";
                return RedirectToAction("ResetPass", "Customer");
            }
            var status = repObj.ResetPassword(email, obj.newPassword, obj.confirmpassword);
            if (status)
            {
                TempData["BhaijiReturns2"] = "Password Updated Successful!";
                return RedirectToAction("MyDetails", "Customer");
            }
            else
            {
                TempData["BhaijiReturns2"] = "Password update failed!";
                return RedirectToAction("MyDetails", "Customer");
            }

        }

    }
}