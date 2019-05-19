using Infosys.BioKartDAL.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Infosys.BioKartDAL
{
    public class BioKartRepository
    {
        private readonly BiokartContext _context;

        public BioKartRepository(BiokartContext context)
        {
            _context = context;
        }
        
            public double getmykami(int uid)
        {
            var user = (from p in _context.PurchaseDetails
                        where p.Seller == uid
                        select p).ToList();
            double kami = 0.0;
            foreach (var item in user)
            {
                kami += (double)item.TotalAmount;
            }
           
            return kami;

        }
        public int GetNotificationCountF(int uid)
        {
            var user = (from p in _context.Notifications
                        where p.UserId == uid 
                        select p).ToList();
            int count = user.Count;
            return count;

        }
        public int GetNotificationCount(int uid)
        {
            var user = (from p in _context.Notifications
                        where p.UserId == uid 
                        select p).ToList();
            int count = user.Count;
            return count;

        }
        public string ValidateCredentials(string emailId, string password)
        {
            var user = (from p in _context.Users
                        where p.EmailId == emailId
                        select p).FirstOrDefault();
            string roleId = "invalid";
            if (user == null)
            {
                return "invalid";
            }


            if (user.UserPassword == password)
            {
                roleId = user.RoleId;
            }

            return roleId;

        }
        public bool CheckEmailId(string emailId)
        {
            var user = (from p in _context.Users
                        where p.EmailId == emailId
                        select p).FirstOrDefault();

            if (user == null)
            {
                return false;
            }



            return true;

        }


        public bool CustomerRegister(string userName, string emailId, string password, string birthPlace, long mobileNumber, string pinCode, string Address)
        {
            bool status = false;
            try
            {
                Users userObj = new Users();
                userObj.Name = userName;
                userObj.EmailId = emailId;
                userObj.UserPassword = password;
                userObj.Security = birthPlace;
                userObj.PhoneNo = mobileNumber;
                userObj.Pin = pinCode;
                userObj.Address = Address;
                userObj.RoleId = "C";
                userObj.Pan = null;

                _context.Users.Add(userObj);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }


        public bool EmployeeRegister(string userName, string emailId, string password, string birthPlace, long mobileNumber, string pinCode, string Address)
        {
            bool status = false;
            try
            {
                Users userObj = new Users();
                userObj.Name = userName;
                userObj.EmailId = emailId;
                userObj.UserPassword = password;
                userObj.Security = birthPlace;
                userObj.PhoneNo = mobileNumber;
                userObj.Pin = pinCode;
                userObj.Address = Address;
                userObj.RoleId = "E";
                userObj.Pan = null;

                _context.Users.Add(userObj);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }



        public bool FarmerRegister(string userName, string emailId, string password, string birthPlace, string pan, long mobileNumber, string PinCode, string Address)
        {
            bool status = false;

            try
            {

                Users userObj = new Users();
                userObj.Name = userName;
                userObj.EmailId = emailId;
                userObj.UserPassword = password;
                userObj.Security = birthPlace;
                userObj.Pan = pan;
                userObj.PhoneNo = mobileNumber;
                userObj.Pin = PinCode;
                userObj.Address = Address;
                userObj.RoleId = "F";

                _context.Users.Add(userObj);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }


        public bool SecurityCheckPassword(string emailId, string birthplace)
        {
            var user = (from p in _context.Users
                        where p.EmailId == emailId && p.Security.ToLower() == birthplace.ToLower()
                        select p).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }



        public bool ResetPassword(string emailId, string newpass, string confirmpass)
        {
            var user = (from p in _context.Users
                        where p.EmailId == emailId
                        select p).FirstOrDefault();
            if (newpass == confirmpass)
            {
                user.UserPassword = newpass;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }
        public bool CollectFeedback(string emailId, string description, string name)
        {
            bool value = false;
            try
            {
                var user = (from p in _context.Feedback
                            where p.EmailId == emailId
                            select p).FirstOrDefault();
                if (user == null)
                {
                    Feedback obj = new Feedback();

                    obj.EmailId = emailId;
                    obj.Description = description;
                    obj.Name = name;
                    _context.Feedback.Add(obj);
                    _context.SaveChanges();
                    value = true;


                }
                else
                {
                    user.Description = description;
                    user.Name = name;

                    _context.SaveChanges();
                    value = true;


                }
            }
            catch (Exception et)
            {

                value = false;
            }
            return value;


        }
        public List<Requests> GetRequests(int CId)
        {
            try
            {
                List<Requests> userObj = (from p in _context.Requests
                                          where p.Cid == CId
                                          select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<PurchaseDetails> GetPurchaseDetails(int CId)
        {
            try
            {
                List<PurchaseDetails> userObj = (from p in _context.PurchaseDetails
                                                 where p.Buyer == CId
                                                 orderby p.PurchaseId descending
                                                 select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool CheckIfProcessed(int rid)
        {
            try
            {
                var userObj = (from p in _context.AdminForwardedRequest
                               where p.CustomerRid == rid && p.Status == "C"
                               select p).FirstOrDefault();
                if (userObj != null)
                {
                    return true;
                }
                else
                    return false;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<AdminForwardedRequest> GetAllRequests()
        {
            try
            {
                List<AdminForwardedRequest> userObj = (from p in _context.AdminForwardedRequest
                                                       where p.Status!="C"
                                                       orderby p.Status descending
                                                       select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Notifications> GetNotifications(int CId)
        {
            try
            {
                List<Notifications> userObj = (from p in _context.Notifications
                                               where p.UserId == CId /*|| p.UserId ==0*/
                                               orderby p.Created descending
                                               select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Notifications> GetNotificationsF(int CId)
        {
            try
            {
                List<Notifications> userObj = (from p in _context.Notifications
                                               where p.UserId == CId/* || p.UserId == 1*/
                                               orderby p.Created descending
                                               select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool DeleteNotification(int nid)
        {
            try
            {
                var userObj = (from p in _context.Notifications
                               where p.Nid == nid
                               select p).FirstOrDefault();
                if(userObj.UserId!=0 && userObj.UserId!=1)
                {

                }
                _context.Notifications.Remove(userObj);

                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public bool DeleteAllNotification(int cid)
        {
            try
            {
                var userObj = (from p in _context.Notifications
                               where p.UserId == cid
                               select p).ToList();
                foreach (var item in userObj)
                {
                    _context.Notifications.Remove(item);
                    _context.SaveChanges();
                }


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Requests> GetAllOpenRequests()
        {
            try
            {
                List<Requests> userObj = (from p in _context.Requests
                                          where p.ForwardStatus != "Y"

                                          select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string GetRoleId(int cidn)
        {
            var query = (from c in _context.Users
                         where c.Uid == cidn
                         select c.RoleId).FirstOrDefault();
            return query;
        }


        public List<PurchaseDetails> Topbuyers()
        {
            try
            {
                List<PurchaseDetails> userObj = (from p in _context.PurchaseDetails
                                                 where p.QuantityPurchased > 1
                                                 orderby p.QuantityPurchased descending
                                                 select p).Take(5).ToList();


                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public bool CollectRequest(Requests feed, int cidn)
        {
            bool value = false;
            try
            {
                var db = (from tpp in _context.Requests
                          where tpp.Cid == cidn
                          select tpp).ToList();

                Requests Prod = (from t in db
                                 where t.Item == feed.Item
                                 select t).FirstOrDefault();
                if (Prod == null)
                {
                    Requests obj = new Requests();

                    obj.Cid = cidn;
                    obj.Item = feed.Item;
                    obj.Quantity = feed.Quantity;
                    obj.ForwardStatus = "N";
                    _context.Requests.Add(obj);
                    _context.SaveChanges();
                    value = true;


                }
                else
                {

                    Prod.Quantity = feed.Quantity;
                    Prod.ForwardStatus = "N";

                    _context.SaveChanges();
                    value = true;


                }
            }
            catch (Exception et)
            {

                value = false;
            }
            return value;
        }


        public List<Users> GetAllCustomers()
        {
            try
            {
                List<Users> oList = (from p in _context.Users
                                     where p.RoleId == "C"
                                     select p).ToList();
                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<AdminForwardedRequest> GetAllForwardedRequests()
        {
            try
            {
                List<AdminForwardedRequest> oList = (from p in _context.AdminForwardedRequest
                                                     orderby p.Arid descending
                                                     select p).ToList();
                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Users> GetAllFarmers()
        {
            try
            {
                List<Users> oList = (from p in _context.Users
                                     where p.RoleId == "F"
                                     select p).ToList();
                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Users> GetAllEmployees()
        {
            try
            {
                List<Users> oList = (from p in _context.Users
                                     where p.RoleId == "E"
                                     select p).ToList();
                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<FarmerStock> GetWareHouseStock()
        {
            try
            {
                
                List<FarmerStock> oList = (from p in _context.FarmerStock
                                           where p.Quantity > 0 && p.CategoryId != null
                                           select p).ToList();
                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<FarmerStock> NearestFarmerForAll(int cidn)
        {
            List<FarmerStock> Pinlist = new List<FarmerStock>();
            try
            {



                var pin = (from c in _context.Users
                           where c.Uid == cidn
                           select c.Pin).First();

                int P = Convert.ToInt32(pin);

                Pinlist = (from a in _context.FarmerStock
                           
                           orderby Math.Abs((Convert.ToInt32(a.Pincode)) - P)
                           select a).ToList();


            }

            catch
            {

                Pinlist = null;
            }

            return Pinlist;
        }
        public List<FarmerStock> NearestFarmer(int cidn,int? category)
        {
            List<FarmerStock> Pinlist = new List<FarmerStock>();
            try
            {
               
               

                    var pin = (from c in _context.Users
                               where c.Uid == cidn
                               select c.Pin).First();

                    int P = Convert.ToInt32(pin);

                    Pinlist = (from a in _context.FarmerStock
                               where a.CategoryId==category
                               orderby Math.Abs((Convert.ToInt32(a.Pincode)) - P)
                               select a).ToList();
                
               
            }

            catch
            {

                Pinlist = null;
            }

            return Pinlist;
        }

        public List<PurchaseDetails> GetMyOrders(string email)
        {
            try
            {
                var query = (from p in _context.Users
                             where p.EmailId == email
                             select p).FirstOrDefault();
                List<PurchaseDetails> oList = (from q in _context.PurchaseDetails
                                               where q.Buyer == query.Uid
                                               select q).ToList();

                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public Users GetUserDetails(int Uid)
        {
            try
            {
                var userObj = (from p in _context.Users
                               where p.Uid == Uid
                               select p).FirstOrDefault();


                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        //updateuserdetails
        //servicecart
        public string GetNextProductId()
        {
            string productId = _context.FarmerStock.OrderByDescending(x => x.Uid).Select(x => x.Uid).FirstOrDefault().ToString();
            int id = Convert.ToInt32(productId.Substring(1, productId.Length - 1)) + 1;
            return "P" + id.ToString();
        }
        public bool AddProduct(FarmerStock product, int cidn)

        {
            bool value = false;
            try
            {
                var query = (from c in _context.FarmerStock
                             where c.Uid == cidn && c.Item == product.Item && c.PricePerUnit == product.PricePerUnit
                             select c).FirstOrDefault();

                var Pincode = (from c in _context.Users
                               where c.Uid == cidn
                               select c.Pin).First();



                if (query == null)
                {
                    FarmerStock objj = new FarmerStock();
                    objj.Uid = cidn;
                    objj.Quantity = product.Quantity;
                    objj.Item = product.Item;
                    objj.PricePerUnit = product.PricePerUnit;
                    objj.CategoryId = product.CategoryId;
                    objj.Pincode = Pincode;

                    _context.FarmerStock.Add(objj);
                    _context.SaveChanges();
                    value = true;
                }
                else
                {
                    int a = query.Quantity;

                    query.Quantity = a + product.Quantity;

                    query.Quantity = a + product.Quantity;



                    _context.FarmerStock.Update(query);
                    _context.SaveChanges();
                    value = true;

                }

            }

            catch (Exception e)
            {
                value = false;
            }
            return value;
        }
        public bool AddPastAuctionResult(PastAuctionResult party, int aid)
        {
            try
            {
                party.AuctionId = aid;
                _context.PastAuctionResult.Add(party);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool AddAuctionProduct(Auctionstock product, int cidn)

        {
            bool value = false;
            try
            {
                var query = (from c in _context.Auctionstock
                             where c.Uid == cidn && c.Item == product.Item && c.PricePerUnit == product.PricePerUnit
                             select c).FirstOrDefault();

                if (query == null)
                {
                    Auctionstock objj = new Auctionstock();
                    objj.Uid = cidn;
                    objj.Quantity = product.Quantity;
                    objj.Item = product.Item;
                    objj.PricePerUnit = product.PricePerUnit;
                    objj.CategoryId = product.CategoryId;


                    _context.Auctionstock.Add(objj);
                    _context.SaveChanges();
                    value = true;
                }
                else
                {
                    int a = query.Quantity;

                    query.Quantity = a + product.Quantity;

                    query.Quantity = a + product.Quantity;



                    _context.Auctionstock.Update(query);
                    _context.SaveChanges();
                    value = true;

                }

            }

            catch (Exception e)
            {
                value = false;
            }
            return value;
        }










        public bool UpdateProduct(Models.FarmerStock userObj, int cidn)
        {
            try
            {
                var catObjFromDB = _context.FarmerStock.Where(x => x.Uid == cidn).Select(x => x).FirstOrDefault();
                catObjFromDB.Quantity = userObj.Quantity;
                catObjFromDB.Item = userObj.Item;
                catObjFromDB.PricePerUnit = userObj.PricePerUnit;
                catObjFromDB.CategoryId = userObj.CategoryId;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAuctionProduct(Models.Auctionstock userObj, int cidn)
        {
            try
            {
                var catObjFromDB = _context.Auctionstock.Where(x => x.Uid == cidn).Select(x => x).FirstOrDefault();
                catObjFromDB.Quantity = userObj.Quantity;
                catObjFromDB.Item = userObj.Item;
                catObjFromDB.PricePerUnit = userObj.PricePerUnit;
                catObjFromDB.CategoryId = userObj.CategoryId;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool SoldStock(int sid, string item, int quantitysold)
        {
            try
            {
                var catObjFromDB = (from p in _context.FarmerStock
                                    where p.Uid == sid && p.Item == item
                                    select p).FirstOrDefault();
                catObjFromDB.Quantity = catObjFromDB.Quantity - quantitysold;

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteProduct(int cidn)
        {
            bool status = false;
            try
            {
                FarmerStock prodObj = _context.FarmerStock.Find(cidn);

                _context.FarmerStock.Remove(prodObj);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }

        public bool PurchaseProduct(Models.PurchaseDetails purchaseDetails)
        {
            try
            {
                _context.PurchaseDetails.Add(purchaseDetails);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                // string a = e.Message;
                //string b = "abc";
                return false;
            }
        }
        public int GetStock(int sid, string item)
        {
            try
            {
                var query = (from p in _context.FarmerStock
                             where p.Uid == sid && p.Item == item
                             select p.Quantity).FirstOrDefault();

                return query;
            }
            catch (Exception e)
            {
                // string a = e.Message;
                //string b = "abc";
                return 0;
            }
        }
        public bool SuccessBuyNotification(string item, int cid, DateTime? delivery, int sid, params string[]  type1)
        {
            try
            {
                if(type1.Length==0)
                {

              
                Notifications notiObj = new Notifications();
                notiObj.UserId = cid;
                notiObj.Description = "Hurray! Your order for " + item + " has been recieved. It'll be delivered to you by " + delivery;
                notiObj.Created = DateTime.Now;

                _context.Notifications.Add(notiObj);
                _context.SaveChanges();
                Notifications notiObjFarmer = new Notifications();
                notiObjFarmer.UserId = sid;
                notiObjFarmer.Description = "Hurray! A customer has booked an order for " + item + " from you! A BioKart Agent will pick it from you.";
                notiObjFarmer.Created = DateTime.Now;

                _context.Notifications.Add(notiObjFarmer);
                _context.SaveChanges();
                }
                else if(type1[0]=="Auction")
                {
                    Notifications notiObj = new Notifications();
                    notiObj.UserId = cid;
                    notiObj.Description = "Hurray! You won the auction for " + item + ". It'll be delivered to you by " + delivery;
                    notiObj.Created = DateTime.Now;

                    _context.Notifications.Add(notiObj);
                    _context.SaveChanges();
                    Notifications notiObjFarmer = new Notifications();
                    notiObjFarmer.UserId = sid;
                    notiObjFarmer.Description = "Hurray! your auction for " + item + " is successfully! A BioKart agent will pick the stock from you";
                    notiObjFarmer.Created = DateTime.Now;

                    _context.Notifications.Add(notiObjFarmer);
                    _context.SaveChanges();
                }
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool SuccessAuctionBuyNotification(string item, int cid, DateTime? delivery, int sid)
        {
            try
            {
                Notifications notiObj = new Notifications();
                notiObj.UserId = cid;
                notiObj.Description = "Hurray! You won the auction for " + item + ". It'll be delivered to you by " + delivery;
                notiObj.Created = DateTime.Now;

                _context.Notifications.Add(notiObj);
                _context.SaveChanges();
                Notifications notiObjFarmer = new Notifications();
                notiObjFarmer.UserId = sid;
                notiObjFarmer.Description = "Hurray! your auction for " + item + " has been closed successfully! A BioKart agent will pick the stock from you";
                notiObjFarmer.Created = DateTime.Now;

                _context.Notifications.Add(notiObjFarmer);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public string GetCustomerName(int cidn)
        {
            try
            {
                var query = (from p in _context.Users
                             where p.Uid == cidn
                             select p.Name).FirstOrDefault();
                return query;
            }
            catch (Exception e)
            {

                return null;
            }
        }


        public Users GetEmployeeByEmail(string email)
        {
            try
            {
                var userObj = (from p in _context.Users
                               where p.EmailId == email
                               select p).FirstOrDefault();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool UpdateEmployeeDetails(Users userObj)
        {
            try
            {
                var catObjFromDB = _context.Users.Where(x => x.EmailId == userObj.EmailId).Select(x => x).FirstOrDefault();
                catObjFromDB.Name = userObj.Name;
                catObjFromDB.Pan = userObj.Pan;
                catObjFromDB.UserPassword = userObj.UserPassword;
                catObjFromDB.Address = userObj.Address;
                catObjFromDB.Pin = userObj.Pin;
                catObjFromDB.PhoneNo = userObj.PhoneNo;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public bool DeleteEmployee(Users catObj)
        {
            try
            {
                var employeeToBeDeleted = _context.Users.Where(x => x.EmailId == catObj.EmailId).FirstOrDefault();
                _context.Users.Remove(employeeToBeDeleted);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddCart(FarmerStock product, int cidn)
        {
            bool value = false;
            try
            {
                var user = (from p in _context.Cart
                            where p.Buyer == cidn
                            select p).ToList();

                Cart Prod = (from t in user
                             where t.ItemName == product.Item
                             select t).FirstOrDefault();


                if (Prod == null)
                {
                    Cart obj = new Cart();
                    obj.Buyer = cidn;
                    obj.Seller = product.Uid;
                    obj.ItemName = product.Item;
                    obj.PricePerUnit = product.PricePerUnit;

                    _context.Cart.Add(obj);
                    _context.SaveChanges();
                    value = true;
                }
                else
                {
                    value = true;

                }
            }
            catch (Exception e)
            {
                value = false;
            }
            return value;
        }



        public List<Cart> GetYourCart(int cidn)
        {
            try
            {
                List<Cart> oList = (from p in _context.Cart
                                    where p.Buyer == cidn
                                    select p).ToList();
                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public List<Auctioncart> GetYourAuctionCart(int cidn)
        {
            try
            {
                List<Auctioncart> oList = (from p in _context.Auctioncart
                                           where p.Buyer == cidn
                                           select p).ToList();
                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<PastAuctionResult> Pastauction()
        {
            try
            {
                List<PastAuctionResult> oList = (from p in _context.PastAuctionResult
                                                 select p).ToList();
                return oList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Models.Categories> GetCategories()
        {
            try
            {
                List<Models.Categories> query = (from c in _context.Categories
                                                 orderby c.CategoryId
                                                 select c).ToList();
                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool AddCategory(Categories catObj)
        {
            try
            {
                _context.Categories.Add(catObj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCategory(Categories catObj)
        {
            try
            {
                var catObjFromDB = _context.Categories.Where(x => x.CategoryId == catObj.CategoryId).Select(x => x).FirstOrDefault();
                catObjFromDB.CategoryName = catObj.CategoryName;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCategory(Categories catObj)
        {
            try
            {
                var categoryToBeDeleted = _context.Categories.Where(x => x.CategoryId == catObj.CategoryId).FirstOrDefault();
                _context.Categories.Remove(categoryToBeDeleted);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }




        public bool UpdateFarmerDetails(Users userObj)
        {
            try
            {
                var catObjFromDB = _context.Users.Where(x => x.EmailId == userObj.EmailId).Select(x => x).FirstOrDefault();
                catObjFromDB.Name = userObj.Name;
                catObjFromDB.Address = userObj.Address;
                catObjFromDB.Pin = userObj.Pin;
                catObjFromDB.PhoneNo = userObj.PhoneNo;
                catObjFromDB.Pan = userObj.Pan;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteFarmer(Users catObj)
        {
            try
            {
                var farmerToBeDeleted = _context.Users.Where(x => x.EmailId == catObj.EmailId).FirstOrDefault();
                _context.Users.Remove(farmerToBeDeleted);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool UpdateCustomerDetails(Users userObj)
        {
            try
            {
                var catObjFromDB = _context.Users.Where(x => x.EmailId == userObj.EmailId).Select(x => x).FirstOrDefault();
                catObjFromDB.Name = userObj.Name;
                catObjFromDB.Address = userObj.Address;
                catObjFromDB.Pin = userObj.Pin;
                catObjFromDB.PhoneNo = userObj.PhoneNo;
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCustomer(Users catObj)
        {
            try
            {
                var farmerToBeDeleted = _context.Users.Where(x => x.EmailId == catObj.EmailId).FirstOrDefault();
                _context.Users.Remove(farmerToBeDeleted);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DropRequest(Requests catObj)
        {
            try
            {
                var requesttobedeleted = _context.Requests.Where(x => x.Rid == catObj.Rid).FirstOrDefault();
                Notifications notiObj = new Notifications();
                notiObj.UserId = catObj.Cid;
                notiObj.Description = "Your Request with id: " + catObj.Rid + " was cancelled by BioKart Admin. Inconvinence is regretted.";
                notiObj.Created = DateTime.Now;

                ; _context.Notifications.Add(notiObj);

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DropOpenRequest(AdminForwardedRequest catObj)
        {
            try
            {
                var requesttobedeleted = _context.AdminForwardedRequest.Where(x => x.Arid == catObj.Arid).FirstOrDefault();
                _context.AdminForwardedRequest.Remove(requesttobedeleted);
                _context.SaveChanges();
                Notifications notiObj = new Notifications();
                notiObj.UserId = catObj.Cid;
                notiObj.Description = "Your Request with id: " + catObj.CustomerRid + " was cancelled by BioKart Admin. Inconvinence is regretted.";
                notiObj.Created = DateTime.Now;

                _context.Notifications.Add(notiObj);

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteFromCart(Cart obj, int cidn)
        {
            bool status = false;
            try
            {
                var query = (from c in _context.Cart
                             where c.Buyer == cidn && c.ItemName == obj.ItemName && c.Cartid == c.Cartid
                             select c).FirstOrDefault();

                _context.Cart.Remove(query);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }
        public bool DeleteFromCartByName(string item, int cidn)
        {
            bool status = false;
            try
            {
                var query = (from c in _context.Cart
                             where c.Buyer == cidn && c.ItemName == item
                             select c).FirstOrDefault();

                _context.Cart.Remove(query);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }
        public bool UpdateFromAuctionItemByName(string item, int seller, int aucid)
        {
            bool status = false;
            try
            {
                var query = (from c in _context.AuctionItem
                             where c.SellerUid == seller && c.Item == item && c.AuctionId == aucid
                             select c).FirstOrDefault();
                query.AuctionStatus = "C";
                _context.AuctionItem.Update(query);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }


        public bool DeleteRequest(int? rid)
        {
            bool status = false;
            try
            {
                Requests cObj = _context.Requests.Find(rid);
                //Cart ToBeDeleted = _context.Cart.Where(x => x.Cartid == obj.Cartid).FirstOrDefault();

                _context.Requests.Remove(cObj);
                _context.SaveChanges();
                status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }


        public bool UpdateRequestDetails(Requests userObj, int cidn)
        {
            try
            {

                var reqObjFromDB = _context.Requests.Where(x => x.Rid == userObj.Rid).Select(x => x).FirstOrDefault();
                reqObjFromDB.Cid = cidn;
                reqObjFromDB.Quantity = userObj.Quantity;
                reqObjFromDB.Item = userObj.Item;

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateRequestStatus(Requests userObj)
        {
            try
            {
                var reqObjFromDB = _context.Requests.Where(x => x.Rid == userObj.Rid).Select(x => x).FirstOrDefault();
                reqObjFromDB.ForwardStatus = "Y";
                reqObjFromDB.Quantity = userObj.Quantity;
                reqObjFromDB.Item = userObj.Item;

                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<FarmerStock> GetFarmerStock(int ci)
        {
            try
            {
                var userObj = (from p in _context.FarmerStock
                               where p.Uid == ci
                               select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Auctionstock> GetAuctionStock(int ci)
        {
            try
            {
                var userObj = (from p in _context.Auctionstock
                               where p.Uid == ci
                               select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Auctionstock> GetAuctionStock()
        {
            try
            {
                var userObj = (from p in _context.Auctionstock
                               where p.CategoryId != null && p.Quantity > 0
                               select p).ToList();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool DeleteStock(int uid, string item)
        {
            try
            {
                var query = (from c in _context.FarmerStock
                             where c.Uid == uid
                             select c).ToList();
                var prod = (from dc in query
                            where dc.Item == item
                            select dc).FirstOrDefault();

                _context.FarmerStock.Remove(prod);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteAuctionStock(int uid, string item)
        {
            try
            {
                var query = (from c in _context.Auctionstock
                             where c.Uid == uid
                             select c).ToList();
                var prod = (from dc in query
                            where dc.Item == item
                            select dc).FirstOrDefault();

                _context.Auctionstock.Remove(prod);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Users GetEmployeeById(int cidn)
        {
            try
            {
                var userObj = (from p in _context.Users
                               where p.Uid == cidn
                               select p).FirstOrDefault();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool CloseRequest(int? cidn, int fid)
        {
            try
            {
                var userObj = (from p in _context.AdminForwardedRequest
                               where p.Arid == cidn
                               select p).FirstOrDefault();
                userObj.Status = "C";
                userObj.FarmerId = fid;
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public int? GetCustomerRID(int? cidn)
        {
            try
            {
                int? userObj = (from p in _context.AdminForwardedRequest
                                where p.Arid == cidn
                                select p.CustomerRid).FirstOrDefault();

                return userObj;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int GetCustomerID(int? cidn)
        {
            try
            {
                int userObj = (from p in _context.AdminForwardedRequest
                               where p.Arid == cidn
                               select p.Cid).FirstOrDefault();

                return userObj;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public int GetFarmer(int auctionId)
        {
            try
            {
                int userObj = (from p in _context.AuctionItem
                               where p.AuctionId==auctionId
                               select p.SellerUid).FirstOrDefault();

                return userObj;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public bool PushSuccessNotification(int? cidn, int cid, int fid)
        {
            try
            {
                Notifications notiObj = new Notifications();
                notiObj.UserId = cid;
                notiObj.Description = "Hurray! Your Request with id: " + cidn + " has been processed by Farmer #" + fid;
                notiObj.Created = DateTime.Now;

                _context.Notifications.Add(notiObj);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Users GetCustomerById(int? cidn)
        {
            try
            {
                var userObj = (from p in _context.Users
                               where p.Uid == cidn
                               select p).FirstOrDefault();

                return userObj;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool SendRequestToFarmer(Requests obj)
        {
            try
            {
                AdminForwardedRequest newObj = new AdminForwardedRequest();
                newObj.Cid = obj.Cid;
                newObj.Quantity = obj.Quantity;
                newObj.Item = obj.Item;
                newObj.CustomerRid = obj.Rid;
                newObj.Status = "O";
                _context.AdminForwardedRequest.Add(newObj);
                //notifyfarmers
                //Notifications notiObj = new Notifications();
                //notiObj.UserId =1;
                //notiObj.Description = "A new Request has been forwarded. Check if you can provide the required stock";
                //notiObj.Created = DateTime.Now;

                //_context.Notifications.Add(notiObj);
                _context.SaveChanges();
                return true;




            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool ForwardedNotification(Requests obj)
        {
            try
            {
                Notifications notiObj = new Notifications();
                notiObj.UserId = obj.Cid;
                notiObj.Description = "Hurray! Your Request with id: " + obj.Rid + " has been forwarded :)";
                notiObj.Created = DateTime.Now;

                _context.Notifications.Add(notiObj);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool SaveAuctionItem(AuctionItem obj)
        {
            try
            {
                //saveitemforauction

                _context.AuctionItem.Add(obj);
                int a = obj.SellerUid;
                _context.SaveChanges();
                //updatingstock
                var query = (from p in _context.Auctionstock
                             where p.Uid == obj.SellerUid && p.Item == obj.Item
                             select p).FirstOrDefault();
                if (query != null)
                {
                    query.Quantity -= obj.FinalQuantity;
                    _context.Auctionstock.Update(query);
                    _context.SaveChanges();
                }
                //sending notification
                //Notifications notiObj1 = new Notifications();
                //notiObj1.UserId = 0;
                //notiObj1.Description = "A new Auction has been started for " + obj.Item + " .Bid to Win!";
                //notiObj1.Created = DateTime.Now;

                //_context.Notifications.Add(notiObj1);
                //_context.SaveChanges();

                //for farmer
                Notifications notiObj = new Notifications();
                notiObj.UserId = a;
                notiObj.Description = "Hurray! Your item " + obj.Item + " has been added to a new auction. Wait for more updates";
                notiObj.Created = DateTime.Now;

                _context.Notifications.Add(notiObj);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }





        public bool SaveAuctionBid(AuctionBid obj)
        {
            try
            {
                //saveitemforBid
                _context.AuctionBid.Add(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public decimal HighestBid(int id)
        {
            try
            {
                var query = (from p in _context.AuctionBid
                             where p.AuctionId == id
                             select p).Max(p => p.BidAmount);
                
                // var query = 100;
                return query;
            }
            catch (Exception e)
            {
                var query = 0;
                return query;
            }

        }

        public List<AuctionBid> HighestBidofEachBidder(int id)
        {
            try
            {
                List<AuctionBid> l1 = new List<AuctionBid>();
                //fetching unique ids
                var query = (from p in _context.AuctionBid
                             where p.AuctionId == id
                             select p.BidderId).Distinct();
                foreach (var item in query)
                {
                    var query1 = (from p in _context.AuctionBid
                                  where p.AuctionId == id && p.BidderId == item
                                  select p).Max(p => p.BidAmount);
                    var query2 = (from p in _context.AuctionBid
                                  where p.AuctionId == id && p.BidderId == item && p.BidAmount == query1
                                  select p).FirstOrDefault();
                    l1.Add(query2);
                }
                var query3 = (from p in l1
                              orderby p.BidAmount descending
                              select p).ToList();


                return query3;
            }
            catch (Exception e)
            {

                return null;
            }

        }
        public decimal TotalBid(int id)
        {
            try
            {
                var query = (from p in _context.AuctionBid
                             where p.AuctionId == id
                             select p.BidderId).Distinct().Count();

                // var query = 100;
                return query;
            }
            catch (Exception e)
            {
                var query = 0;
                return query;
            }

        }

        public decimal HighestBidUser(int id, int aid)
        {
            try
            {
                decimal query = 0;
                var q = (from p in _context.AuctionBid
                         where p.BidderId== id && p.AuctionId == aid
                         select p).FirstOrDefault();
                if (q != null)
                {
                    query = (from p in _context.AuctionBid
                             where p.BidderId == id && p.AuctionId == aid
                             select p).Max(p => p.BidAmount);
                    // var query = 100;

                    return query;
                }

                return 0;
            }
            catch (Exception e)
            {
                var query = 0;
                return query;
            }

        }


        public List<decimal> PreviousBids(int id1, int id2)
        {
            try
            {
                var query = (from p in _context.AuctionBid
                             where p.AuctionId == id1 && p.BidderId == id2
                             orderby p.BidId descending
                             select p.BidAmount).Take(5).ToList();
                // var query = 100;
                return query;
            }
            catch (Exception e)
            {

                return null;
            }

        }



        public List<AuctionItem> GetAllAuctionsByEmpid(int empid)
        {
            try
            {
                var query = (from p in _context.AuctionItem
                             where p.EmpId == empid && p.AuctionStatus != "C"
                             select p).ToList();

                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }




        public List<AuctionBid> GetAllBid(int aucid)
        {
            try
            {
                var query = (from p in _context.AuctionBid
                             where p.AuctionId == aucid
                             orderby p.BidAmount descending
                             select p).ToList();

                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public string GetFarmerPincode(int empid)
        {
            try
            {
                var query = (from p in _context.Users
                             where p.Uid == empid
                             select p.Pin).FirstOrDefault();

                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public bool isaBiddedItem(int aucid)

        {
            var query = (from p in _context.AuctionBid
                         where p.AuctionId == aucid
                         select p).FirstOrDefault();
            if (query == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool DeleteAuction(AuctionItem obj)
        {
            try
            {

                //updatingstock
                var query = (from p in _context.Auctionstock
                             where p.Uid == obj.SellerUid && p.Item == obj.Item
                             select p).FirstOrDefault();
                if (query != null)
                {
                    query.Quantity += obj.FinalQuantity;
                    _context.Auctionstock.Update(query);
                    _context.SaveChanges();
                    //notification 
                    Notifications notiObj = new Notifications();
                    notiObj.UserId = obj.SellerUid;
                    notiObj.Description = " Restocked! Your item's (" + obj.Item + ") auction has been cancelled.";
                    notiObj.Created = DateTime.Now;

                    _context.Notifications.Add(notiObj);
                    _context.SaveChanges();
                    var query2 = (from p in _context.AuctionItem
                                  where p.AuctionId == obj.AuctionId
                                  select p).FirstOrDefault();
                    _context.AuctionItem.Remove(query2);
                    _context.SaveChanges();
                }
                else
                {
                    //he removed
                    Auctionstock product = new Auctionstock();
                    product.Item = obj.Item;
                    product.Uid = obj.SellerUid;
                    product.Quantity = obj.FinalQuantity;
                    product.PricePerUnit = obj.PricePerUnit;

                    AddAuctionProduct(product, obj.SellerUid);
                    //send notification for category update
                    Notifications notiObj = new Notifications();
                    notiObj.UserId = obj.SellerUid;
                    notiObj.Description = " Restocked! Your item's (" + obj.Item + ") auction has been cancelled. Update Category!";
                    notiObj.Created = DateTime.Now;

                    _context.Notifications.Add(notiObj);
                    _context.SaveChanges();

                    Notifications notiObj1 = new Notifications();
                    notiObj1.UserId = obj.SellerUid;
                    notiObj1.Description = "Stock for " + obj.Item + " will not be up for auction until you update the category!";
                    notiObj1.Created = DateTime.Now;

                    _context.Notifications.Add(notiObj1);
                    _context.SaveChanges();
                    var query2 = (from p in _context.AuctionItem
                                  where p.AuctionId == obj.AuctionId
                                  select p).FirstOrDefault();
                    _context.AuctionItem.Remove(query2);
                    _context.SaveChanges();
                }
                //var query2 = (from p in _context.AuctionItem
                //              where p.AuctionId == obj.AuctionId
                //              select p).FirstOrDefault();
                //_context.AuctionItem.Remove(query2);
                //_context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public List<AuctionItem> GetAllOnAuctions()
        {
            try
            {
                var query = (from p in _context.AuctionItem
                             where p.EndDate > DateTime.Now && p.AuctionStatus != "C"
                             select p).ToList();

                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool AddToAuctionCart(Auctioncart finalObj)

        {
            try
            {

                _context.Auctioncart.Add(finalObj);
                _context.SaveChanges();
                //sending notification
                Notifications notiObj = new Notifications();
                notiObj.UserId = (int)finalObj.Buyer;
                notiObj.Description = "Hurray! You won the auction for " + finalObj.ItemName + " @ Rs." + finalObj.TotalPrice + " .Please check your Auction Cart!";
                notiObj.Created = DateTime.Now;

                _context.Notifications.Add(notiObj);
                _context.SaveChanges();
                //set status as closed
                var query = (from p in _context.AuctionItem
                             where p.AuctionId == finalObj.AuctionId
                             select p).FirstOrDefault();
                query.AuctionStatus = "CLOSED";
                _context.AuctionItem.Update(query);
                _context.SaveChanges();
                //make it disappear
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public int? GetSellerIdforAuction(int id)
        {
            try
            {
                var query = (from p in _context.AuctionItem
                             where p.AuctionId == id
                             select p.SellerUid).FirstOrDefault();

                return query;
            }
            catch (Exception e)
            {
                return 0;
            }
        }


        public string GetItemNameforAuction(int id)
        {
            try
            {
                var query = (from p in _context.AuctionItem
                             where p.AuctionId == id
                             select p.Item).FirstOrDefault();

                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public int GetQuantityForAuction(int id)
        {
            try
            {
                var query = (from p in _context.AuctionItem
                             where p.AuctionId == id
                             select p.FinalQuantity).FirstOrDefault();

                return query;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public List<Feedback> GetFeedback()
        {
            try
            {
                var query = (from p in _context.Feedback
                             select p).ToList();

                return query;
            }
            catch (Exception e)
            {
                return null;
            }
        }












    }
}
