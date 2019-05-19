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
    public class EmployeeController : Controller
    {
        static int? categoryidforuse = 100;
        static string userpassword11;
        private readonly BioKartRepository _repObj;
        private readonly IMapper _mapper;
        public EmployeeController(BiokartContext context, IMapper mapper)
        {
            _repObj = new BioKartRepository(context);
            _mapper = mapper;
        }

        public IActionResult EmpViewProducts()
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
            var lstEntityProducts = _repObj.GetWareHouseStock();
            List<Models.FarmerStock> lstModelProducts = new List<Models.FarmerStock>();
            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.FarmerStock>(product));
            }
            return View(lstModelProducts);
        }
        public IActionResult GetAllEmployee(Models.EmployeeUser userObj)
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
            string a =_repObj.GetRoleId(uid);
            if(a!="A")
            {
                TempData["NotAdmin"] = "Sorry, only BioKart Admin can manage Employee Records!";
                return RedirectToAction("EmployeeHome", "User");
            }
            var lstEntityEmployee = _repObj.GetAllEmployees();
            List<Models.EmployeeUser> lstModelEmployee = new List<Models.EmployeeUser>();
            foreach (var product in lstEntityEmployee)
            {
                lstModelEmployee.Add(_mapper.Map<Models.EmployeeUser>(product));
            }
            return View(lstModelEmployee);
        }

        public IActionResult UpdateEmployeeDetails(Models.EmployeeUser userObj)
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
            return View(userObj);
        }
        public IActionResult MyDetails()
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
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            var empObj = _repObj.GetEmployeeById(cidn);
            userpassword11 = empObj.UserPassword;
            Models.EmployeeUser obj = _mapper.Map<Models.EmployeeUser>(empObj);
            return View(obj);
        }


        public IActionResult SaveUpdatedEmployeeDetail(Models.EmployeeUser userObj)
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
            if (ModelState.IsValid)
            {
                try
                {
                    
                    status = _repObj.UpdateEmployeeDetails(_mapper.Map<Users>(userObj));
                    if (status)
                        return RedirectToAction("GetAllEmployee");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("UpdateEmployeeDetails", userObj);
        }

        public IActionResult SaveMyUpdatedDetail(Models.EmployeeUser userObj)
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
            if (ModelState.IsValid)
            {
                try
                {
                    userObj.UserPassword = userpassword11;
                    status = _repObj.UpdateEmployeeDetails(_mapper.Map<Users>(userObj));
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
            return View("UpdateEmployeeDetails", userObj);
        }

        public IActionResult EmployeeDetails(Models.EmployeeUser emp)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
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
            return View(emp);
        }

        public IActionResult DeleteEmployee(Models.EmployeeUser userObj)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
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
            return View(userObj);
        }

        public IActionResult SaveDeletionEmployee(Users emp)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));


            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
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
            bool status = false;
            try
            {
                status = _repObj.DeleteEmployee(emp);
                if (status)
                    return RedirectToAction("GetAllEmployee");
                else
                    return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        //--------------------------------------------------------------------------------

        public IActionResult GetAllFarmer(Models.FarmerEmployeeUserD userObj)
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

            var lstEntityFarmer = _repObj.GetAllFarmers();
            List<Models.FarmerEmployeeUserD> lstModelFarmer = new List<Models.FarmerEmployeeUserD>();
            foreach (var product in lstEntityFarmer)
            {
                lstModelFarmer.Add(_mapper.Map<Models.FarmerEmployeeUserD>(product));
            }
            return View(lstModelFarmer);
        }


        public IActionResult FarmerDetails(Models.FarmerEmployeeUserD emp)
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
            return View(emp);
        }
        public IActionResult ResetPass()
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
            return View();
        }
        public IActionResult SaveResetPass(Models.ResetPasswordDashboard obj)
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
            Users obj1 = _repObj.GetUserDetails(uid);
            string email = obj1.EmailId;
            if(obj1.UserPassword!=obj.oldpassword)
            {
                TempData["OhBhaiji"] = "Old Password is Wrong! Please Check";
                return RedirectToAction("ResetPass","Employee");
            }
            if(obj.newPassword!=obj.confirmpassword)
            {
                TempData["OhBhaiji"] = "New Password and Confirm Password do not match! Please Check";
                return RedirectToAction("ResetPass", "Employee");
            }
            if(obj.newPassword == obj.confirmpassword && obj1.UserPassword== obj.newPassword)
            {
                TempData["OhBhaiji"] = "New Password cannot be same as old.";
                return RedirectToAction("ResetPass", "Employee");
            }
            var status =_repObj.ResetPassword(email,obj.newPassword,obj.confirmpassword);
            if(status)
            {
                TempData["BhaijiReturns"] = "Password Updated Successful!";
                return RedirectToAction("MyDetails", "Employee");
            }
            else
            {
                TempData["BhaijiReturns"] = "Password update failed!";
                return RedirectToAction("MyDetails", "Employee");
            }
            
        }
       
        public IActionResult FarmerDetails2(Models.AdminForwardedRequests obj)
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
            if (obj.FarmerId==null)
            {
                return RedirectToAction("approvedrequeststatus");
            }
            
            Models.FarmerEmployeeUserD abc = _mapper.Map<Models.FarmerEmployeeUserD>(_repObj.GetCustomerById(obj.FarmerId));
            return View(abc);
        }

        public IActionResult UpdateFarmerDetails(Models.FarmerEmployeeUserD userObj)
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
            return View(userObj);
        }



        public IActionResult SaveUpdatedFarmerDetail(Models.FarmerEmployeeUserD userObj)
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
            if (ModelState.IsValid)
            {
                try
                {
                    status = _repObj.UpdateFarmerDetails(_mapper.Map<Users>(userObj));
                    if (status)
                        return RedirectToAction("GetAllFarmer");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("UpdateFarmerDetails", userObj);
        }


        public IActionResult DeleteFarmer(Models.FarmerEmployeeUserD userObj)
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
            return View(userObj);
        }

        public IActionResult SaveDeletionFarmer(Users emp)
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
            try
            {
                status = _repObj.DeleteFarmer(emp); ;
                if (status)
                    return RedirectToAction("GetAllFarmer");
                else
                    return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        //------------------------- Customer ------------------------------------

        public IActionResult GetAllCustomer(Models.CustomerEmployeeUserD userObj)
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
            var lstEntityCustomer = _repObj.GetAllCustomers();
            List<Models.CustomerEmployeeUserD> lstModelCustomer = new List<Models.CustomerEmployeeUserD>();
            foreach (var product in lstEntityCustomer)
            {
                lstModelCustomer.Add(_mapper.Map<Models.CustomerEmployeeUserD>(product));
            }
            return View(lstModelCustomer);
        }


        public IActionResult CustomerDetails(Models.CustomerEmployeeUserD emp)
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
            return View(emp);
        }
      

        public IActionResult UpdateCustomerDetails(Models.CustomerEmployeeUserD userObj)
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
            return View(userObj);
        }



        public IActionResult SaveUpdatedCustomerDetail(Models.CustomerEmployeeUserD userObj)
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
            if (ModelState.IsValid)
            {
                try
                {
                    status = _repObj.UpdateCustomerDetails(_mapper.Map<Users>(userObj));
                    if (status)
                        return RedirectToAction("GetAllCustomer");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
            }
            return View("UpdateFarmerDetails", userObj);
        }


        public IActionResult DeleteCustomer(Models.CustomerEmployeeUserD userObj)
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
            return View(userObj);
        }

        public IActionResult SaveDeletionCustomer(Users emp)
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
            try
            {
                status = _repObj.DeleteCustomer(emp); ;
                if (status)
                    return RedirectToAction("GetAllCustomer");
                else
                    return View("Error");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        public IActionResult UpdateMyDetails(Models.EmployeeUser userObj)
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
            return View(userObj);
        }
        public IActionResult ViewRequest()
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
            var lstEntityProducts = _repObj.GetAllOpenRequests();
            if(lstEntityProducts.Count!=0)
            {
                List<Models.Request> lstModelProducts = new List<Models.Request>();
                foreach (var product in lstEntityProducts)
                {
                    lstModelProducts.Add(_mapper.Map<Models.Request>(product));
                }
                return View(lstModelProducts);
            }
            else
            {
                return RedirectToAction("NoRequests","Employee");
            }
            
        }
        public IActionResult NoRequests()
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
            return View();
        }
        public IActionResult AdminForwardedRequestCommit(Models.Request obj)
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
            bool status=_repObj.SendRequestToFarmer(_mapper.Map<Requests>(obj));
            if (status)
            {
                
                _repObj.UpdateRequestStatus(_mapper.Map<Requests>(obj));
                _repObj.ForwardedNotification(_mapper.Map<Requests>(obj));
            }
            return RedirectToAction("ViewRequest","Employee");
        }
        
        public IActionResult CustomerDetails2(Models.Request obj)
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
            int CId = obj.CId;
            Models.CustomerEmployeeUserD abc = _mapper.Map<Models.CustomerEmployeeUserD>(_repObj.GetCustomerById(CId));
            return View(abc);
        }
        public IActionResult DropRequest(Models.Request obj)
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
                bool status = _repObj.DropRequest(_mapper.Map<Requests>(obj));
                if (status)
                {
                    _repObj.DeleteRequest(obj.RId);
                    
                    return RedirectToAction("ViewRequest", "Employee");
                }
                else
                {
                    return View("Error");
                }


            }
            catch (Exception)
            {

                return View("Error");
            }
           
            
        }
        public IActionResult DeleteOpenRequest(Models.AdminForwardedRequests obj)
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
                if (obj.Status=="O")
                {
                    bool status = _repObj.DropOpenRequest(_mapper.Map<AdminForwardedRequest>(obj));
                    if (status)
                    {

                        _repObj.DeleteRequest(obj.CustomerRid);
                        return RedirectToAction("approvedrequeststatus", "Employee");
                    }
                    else
                    {
                        return View("Error");
                    }
                }

                return RedirectToAction("approvedrequeststatus");

            }
            catch (Exception)
            {

                return View("Error");
            }


        }
        
        public IActionResult approvedrequeststatus()
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
                var query = _repObj.GetAllForwardedRequests();
                List<Models.AdminForwardedRequests> l1 = new List<Models.AdminForwardedRequests>();
                foreach (var item in query)
                {
                    l1.Add(_mapper.Map<Models.AdminForwardedRequests>(item));
                }
                return View(l1);
            }
            catch (Exception)
            {

                return View("Error");
            }
            
        }
        public IActionResult ViewAuctionStock()
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

            var lstEntityProducts = _repObj.GetAuctionStock();
            if (lstEntityProducts.Count == 0)
            {
                TempData["NoAuctionStock"] = "No Stock Available for Auction.";
                return RedirectToAction("GetProductForCategory", "Employee");
            }
            List<Models.Auctionstock> lstModelProducts = new List<Models.Auctionstock>();
            foreach (var product in lstEntityProducts)
            {
                lstModelProducts.Add(_mapper.Map<Models.Auctionstock>(product));
            }
            return View(lstModelProducts);
        }


        public IActionResult NoAuctionsStock()
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
            return View();
        }
        public IActionResult Queries()
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
                var query= _repObj.GetFeedback();
                List<Models.FeedBack> l1 = new List<Models.FeedBack>();
                foreach (var item in query)
                {
                    l1.Add(_mapper.Map<Models.FeedBack>(item));
                }
                return View(l1);

            }
            catch (Exception)
            {

                return View("Error");
            }
           
        }
        public IActionResult GoBack()
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
            if (TempData["Sortbythiscategory"] == null)
            {
                return RedirectToAction("GetProductForCategory", "Employee");
            }
            else
            {
                return Redirect("/Employee/GetProductForCategory?categoryId=" + (int)TempData["Sortbythiscategory"]);
            }

        }
        public IActionResult GoBack2()
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
            if (TempData["ReturnToMain"] == null)
            {
                categoryidforuse = null;
                return RedirectToAction("SortResult", "Employee");
            }
            else
            {
                categoryidforuse = (int)TempData["ReturnToMain"];
                return RedirectToAction("SortResult", "Employee");
            }

        }
        public IActionResult GetProductForCategory(byte? categoryId)
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
            TempData["Sortbythiscategory"] = categoryId;
            ViewBag.CategoryList = _repObj.GetCategories();
            var productList = _repObj.GetWareHouseStock();
            var products = new List<Models.FarmerStock>();
            foreach (var product in productList)
            {
                products.Add(_mapper.Map<Models.FarmerStock>(product));
            }
            if (categoryId == null)
            {
                var filteredProducts2 = products;
                return View(filteredProducts2);
            }
            var filteredProducts = products.Where(model => model.CategoryId == categoryId);

            return View(filteredProducts);
        }
        public IActionResult SortResult()
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
            int cidn = Convert.ToInt32(HttpContext.Session.GetString("uid"));
            //int? category = (int)TempData["Sortbythiscategory"];
            if (categoryidforuse != 100)
            {
                if (categoryidforuse == null)
                {
                    var lstEntityProducts2 = _repObj.NearestFarmerForAll(cidn);
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
                    var lstEntityProducts2 = _repObj.NearestFarmer(cidn, categoryidforuse);
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
            if (TempData["Sortbythiscategory"] == null)
            {
                var lstEntityProducts2 = _repObj.NearestFarmerForAll(cidn);
                List<Models.FarmerStock> lstModelProducts2 = new List<Models.FarmerStock>();
                foreach (var product in lstEntityProducts2)
                {
                    lstModelProducts2.Add(_mapper.Map<Models.FarmerStock>(product));
                }
                TempData["ReturnToMain"] = null;
                categoryidforuse = 100;
                return View(lstModelProducts2);
            }
            var lstEntityProducts = _repObj.NearestFarmer(cidn, (int?)TempData["Sortbythiscategory"]);
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
            Users obj = _repObj.GetUserDetails(farmer.Uid);
            Models.FarmerDetails up = _mapper.Map<Models.FarmerDetails>(obj);

            return View(up);
        }
        public IActionResult SupplierInfo(Models.PastAuctionResult PROD)
        {
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));

            if (uid == 0)
            {
                return RedirectToAction("Login", "Home");
            }

            Users obj = _repObj.GetUserDetails(PROD.FarmerId);
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
            Users obj = _repObj.GetUserDetails(farmer.Uid);
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
            Users obj = _repObj.GetUserDetails(farmer.Uid);
            Models.FarmerDetails up = _mapper.Map<Models.FarmerDetails>(obj);

            return View(up);
        }


    }
}