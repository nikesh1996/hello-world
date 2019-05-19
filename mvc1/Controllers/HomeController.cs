using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infosys.BioKartMVC.Models;
using Microsoft.AspNetCore.Http;
using Infosys.BioKartDAL;
using Microsoft.AspNetCore.Http;
using Infosys.BioKartDAL.Models;

namespace Infosys.BioKartMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly BioKartRepository _repObj;
        public HomeController(BiokartContext context)
        {
            _repObj = new BioKartRepository(context);
        }


        public IActionResult Index()
        {
            return View();
        }
       

       
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            
            int uid = Convert.ToInt32(HttpContext.Session.GetString("uid"));
           
            if(uid==0)
            {
              
                return View();
            }
            else
            {
                string roleId = _repObj.GetRoleId(uid);
                if (roleId == "F")
                {
                 
                    return Redirect("/User/FarmerHome");
                }
                else if (roleId == "C")
                {
                    
                    return Redirect("/User/CustomerHome");
                }
                else if (roleId == "E")
                {
                    
                    return Redirect("/User/EmployeeHome");
                }
                else if (roleId=="A")
                {
                    return Redirect("/User/AdminHome");
                }
                return View();
            }
           
        }


        public IActionResult CheckRole(IFormCollection frm)
        {

            string userId = frm["username"];
            string password = frm["password"];
            string checkbox = frm["RememberMe"];
            if (checkbox == "on")
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Append("UserId", userId, option);
                Response.Cookies.Append("Password", password, option);
            }
            string u_name = userId.Split('@')[0];
            

            string roleId = _repObj.ValidateCredentials(userId.ToLower(), password);
            if (roleId == "invalid")
            {
                
                TempData["Status"] = "Wrong Credentials";
                return RedirectToAction("Login", "Home");
            }

            Users user = _repObj.GetEmployeeByEmail(userId);
            string name = user.Name.Split()[0];
            if (roleId == "F")
            {
                HttpContext.Session.SetString("username", name);
                //Uid stored in session
                HttpContext.Session.SetString("uid", Convert.ToString(user.Uid));
                return Redirect("/User/FarmerHome");
            }
            else if (roleId == "C")
            {

                HttpContext.Session.SetString("username", name);
                //Uid stored in session
                HttpContext.Session.SetString("uid", Convert.ToString(user.Uid));
                return Redirect("/User/CustomerHome");
            }
            else if (roleId == "E" )
            {
                HttpContext.Session.SetString("username", name);
                //Uid stored in session
                HttpContext.Session.SetString("uid", Convert.ToString(user.Uid));
                return Redirect("/User/EmployeeHome");
            }
            else if (roleId == "A")
            {
                HttpContext.Session.SetString("username", name);
                //Uid stored in session
                HttpContext.Session.SetString("uid", Convert.ToString(user.Uid));
                return Redirect("/User/AdminHome");
            }

            return View("Login");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public IActionResult FeedBack()
        //{
        //    return View();
        //}
        
        public IActionResult SaveFeedBack(Models.FeedBack feed)
        {
            bool status = false;
            //if (ModelState.IsValid)
            //{
                try
                {
                    status = _repObj.CollectFeedback(feed.EmailId.ToLower(), feed.Description, feed.Name);
                    if (status)
                        return RedirectToAction("Index");
                    else
                        return View("Error");
                }
                catch (Exception)
                {
                    return View("Error");
                }
        //    }
        //    return View("Index", feed);
        }
        public IActionResult success()
        {
            return View("SuccessPurchase");
        }
    }
}
