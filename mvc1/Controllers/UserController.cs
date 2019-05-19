using AutoMapper;
using Infosys.BioKartDAL;
using Infosys.BioKartDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infosys.BioKartMVC.Controllers
{
    public class UserController : Controller
    {
        static string emailidforuse11="";
        private readonly BioKartRepository _repObj;
        private readonly IMapper _mapper;
        public UserController(BiokartContext context, IMapper mapper)
        {
            _repObj = new BioKartRepository(context);
            _mapper = mapper;
        }


        public IActionResult CustomerRegisterUser()
        {
            return View();
        }

        public IActionResult SaveCustomerRegisterUser(Models.CustomerUser userObj)
        {
            if (ModelState.IsValid)
            {
                bool emailexistent = _repObj.CheckEmailId(userObj.EmailId.ToLower());
                if (emailexistent == true)
                {
                    TempData["RegisterCustomer"] = "Email Already Registered";
                    return RedirectToAction("CustomerRegisterUser", "user");
                }
                if (userObj.ConfirmPassword != userObj.UserPassword)
                {
                    TempData["RegisterCustomer"] = "Passwords don't match";
                    return RedirectToAction("CustomerRegisterUser", "User");
                }
                bool containsInt = userObj.Birthplace.Any(char.IsDigit);
                if (containsInt)
                {

                    TempData["RegisterCustomer"] = "Invalid Birthplace!";
                    return RedirectToAction("CustomerRegisterUser", "user");

                }
                //    if()



                var returnValue = _repObj.CustomerRegister(userObj.Name, userObj.EmailId.ToLower(), userObj.UserPassword, userObj.Birthplace.ToLower(), userObj.Phno, userObj.Pincode, userObj.Address);
                if (returnValue)
                {
                    TempData["Status"] = "Success! Please login";
                    return RedirectToAction("Login", "Home");
                }
                 
                else
                    return View("Error");
            }
            return View("CustomerRegisterUser");
        }


        public IActionResult FarmerRegisterUser()
        {
            return View();
        }

        public IActionResult SaveFarmerRegisterUser(Models.FarmerUser userObj)
        {
            if (ModelState.IsValid)
            {
                bool emailexistent = _repObj.CheckEmailId(userObj.EmailId.ToLower());
                if(emailexistent==true)
                {
                    TempData["RegisterFarmer"] ="Email Already Registered";
                    return RedirectToAction("FarmerRegisterUser","user");
                }
                if (userObj.ConfirmPassword != userObj.UserPassword)
                {
                    TempData["RegisterFarmer"] = "Passwords don't match";
                    return RedirectToAction("FarmerRegisterUser", "User");
                }
                bool containsInt = userObj.Birthplace.Any(char.IsDigit);
                if (containsInt)
                {

                    TempData["RegisterFarmer"] = "Invalid Birthplace!";
                    return RedirectToAction("FarmerRegisterUser", "user");

                }
                var returnValue = _repObj.FarmerRegister(userObj.Name, userObj.EmailId.ToLower(), userObj.UserPassword, userObj.Birthplace.ToLower(),userObj.PAN, userObj.Phno, userObj.Pincode, userObj.Address);
                if (returnValue)
                {
                    TempData["Status"] = "Success! Please login";
                    return RedirectToAction("Login", "Home");
                }
               
                else
                    return View("Error");
            }
            return View("FarmerRegisterUser");
        }




        public IActionResult RegisterUser()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid != 0)
            {

                return RedirectToAction("Login","Home");
            }
            return View();
        }


        public IActionResult ResetPassword()
        {
           
            return View();
        }

        public IActionResult SaveResetPassword(Models.ResetPassword userObj)
        {
            if (ModelState.IsValid)
            {
                if(!_repObj.CheckEmailId(userObj.EmailId.ToLower()))
                {
                    TempData["Status1"] = "Wrong Email Address";
                    return RedirectToAction("ResetPassword");
                }
                var returnValue = _repObj.SecurityCheckPassword(userObj.EmailId.ToLower(), userObj.Birthplace.ToLower());
                if (returnValue)
                {
                    ViewBag.emailadd = userObj.EmailId;
                    emailidforuse11 = userObj.EmailId;
                    TempData["ema"]= userObj.EmailId;
                    return RedirectToAction("ConfirmPassword", "User");
                }
                else
                {
                    TempData["Status1"] = "Wrong Credentials";
                    return RedirectToAction("ResetPassword");
                }
              
            }
            
            return View("ResetPassword");
        }


        public IActionResult ConfirmPassword()
        {
            return View();
        }

        public IActionResult SaveConfirmPassword(Models.ConfirmPassword userObj)
        {
            //if (ModelState.IsValid)
            //{
                bool id = _repObj.CheckEmailId(userObj.EmailId.ToLower());
                if(id==false)
                {
                    TempData["This1"] = "Wrong Email Address";
                   

                    return RedirectToAction("ResetPassword", "user");
                }
                 //emailidforuse11 = userObj.EmailId;
            var returnValue = _repObj.ResetPassword(emailidforuse11.ToLower(), userObj.UserPassword, userObj.UserPassword1);
                if (returnValue)
                {
                    TempData["Status"] = "Password Reset Success! Please login";

                    return RedirectToAction("Login", "Home");
                }
                    
                else
                {
                    TempData["This1"] = "Passwords don't match";

                    return RedirectToAction("ResetPassword", "user");
                }
               
            //}
            //return View("ResetPassword");
        }

        public IActionResult GetNotifications()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var query = _repObj.GetNotifications(cidn);
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


        public IActionResult AdminHome()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login","Home");
            }
            if (_repObj.GetRoleId(uid) != "A")
            {
                if (_repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }
                if (_repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }
                if (_repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }

            }
            return View();
        }

        public IActionResult EmployeeHome()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if(_repObj.GetRoleId(uid)!="E")
            {
                if(_repObj.GetRoleId(uid)=="C")
                {
                    return RedirectToAction("CustomerHome","User");
                }
                if (_repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
                if (_repObj.GetRoleId(uid) =="F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                
            }
            return View();
        }
        
        public IActionResult FarmerHome()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (_repObj.GetRoleId(uid) != "F")
            {
                if (_repObj.GetRoleId(uid) == "C")
                {
                    return RedirectToAction("CustomerHome", "User");
                }
                if (_repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
                if (_repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }

            }
            int val = _repObj.GetNotificationCountF(uid);
            if (val == 0)
            {
                ViewBag.NotificationsF = "";
                return View();
            }
            else
            {
                ViewBag.NotificationsF = val;
                return View();
            }
        }
        public IActionResult CustomerHome()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (_repObj.GetRoleId(uid) != "C")
            {
                if (_repObj.GetRoleId(uid) == "F")
                {
                    return RedirectToAction("FarmerHome", "User");
                }
                if (_repObj.GetRoleId(uid) == "A")
                {
                    return RedirectToAction("AdminHome", "User");
                }
                if (_repObj.GetRoleId(uid) == "E")
                {
                    return RedirectToAction("EmployeeHome", "User");
                }

            }
            int val=_repObj.GetNotificationCount(uid);
            if(val==0)
            {
                ViewBag.Notifications = "";
                return View();
            }
            else
            {
                ViewBag.Notifications = val;
                return View();
            }
           
           
        }


        public IActionResult EmployeeRegisterUser()
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }
            if (_repObj.GetRoleId(uid) != "E")
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
            return View();
        }


        public IActionResult SaveEmployeeRegisterUser(Models.CustomerUser userObj)
        {
            if (ModelState.IsValid)
            {
                bool emailexistent = _repObj.CheckEmailId(userObj.EmailId.ToLower());
                if (emailexistent == true)
                {
                    TempData["RegisterEmployee"] = "Email Already Registered";
                    return RedirectToAction("EmployeeRegisterUser", "user");
                }
                if (userObj.ConfirmPassword != userObj.UserPassword)
                {
                    TempData["RegisterEmployee"] = "Passwords don't match";
                    return RedirectToAction("EmployeeRegisterUser", "User");
                }
                bool containsInt = userObj.Birthplace.Any(char.IsDigit);
                if (containsInt)
                {

                    TempData["RegisterEmployee"] = "Invalid Birthplace!";
                    return RedirectToAction("CustomerRegisterUser", "user");

                }
                var returnValue = _repObj.EmployeeRegister(userObj.Name, userObj.EmailId.ToLower(), userObj.UserPassword, userObj.Birthplace.ToLower(), userObj.Phno, userObj.Pincode, userObj.Address);
                if (returnValue)
                    return RedirectToAction("GetAllEmployee", "Employee");
                else
                    return View("Error");
            }
            return View("EmployeeRegisterUser");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("uid", "0");
            return Redirect("/home/login");
        }
       



    }
}