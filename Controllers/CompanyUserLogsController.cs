using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyFly.Models;
using EasyFly.PaymentGateway;

namespace EasyFly.Controllers
{
    public class CompanyUserLogsController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();
        UserType abc = new UserType();
        string adminid = "", adminpassword = "", SessionOfUserID = "", SessionOfUserEmail = "";
        private string price;









        // GET: CompanyUserLogs/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        // GET: CompanyUserLogs/Login
        [HttpPost]
        public ActionResult Login(UserLogin userlogin)
        {
            if (ModelState.IsValid)
            {
                var userloginquery = db.CompanyUserLogs.Where(u => u.C_Email.Equals(userlogin.UserEmail)
                                        && u.C_Passkey.Equals(userlogin.Passkey)).FirstOrDefault();

                if (userloginquery != null)
                {
                    Session["User_Email"] = userlogin.UserEmail;
                    Session["User_Type"] = "CompanyUser";
                    return RedirectToAction("SearchForFlight");
                }
                else
                {
                    ViewBag.LoginFailed = "User not found or wrong credentials!";
                    return View();
                }

            }
            return View();
        }











        // GET: CompanyUserLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyUserLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyUserLog companyUserLog)
        {
            string filename = Path.GetFileNameWithoutExtension(companyUserLog.C_PhotoFile.FileName);
            string extension = Path.GetExtension(companyUserLog.C_PhotoFile.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            companyUserLog.C_Photos = "~/Photos/CompanyUsers/" + filename;

            filename = Path.Combine(Server.MapPath("~/Photos/CompanyUsers/"), filename);
            companyUserLog.C_PhotoFile.SaveAs(filename);


            Random rand = new Random();
            int UserID = rand.Next(0, 100000);
            companyUserLog.C_UserID = "C_" + UserID.ToString();

            if (ModelState.IsValid)
            {
                db.CompanyUserLogs.Add(companyUserLog);
                db.SaveChanges();
                return RedirectToAction("Login", "CompanyUserLogs");
            }

            return View(companyUserLog);
        }













        // GET: CompanyUserLogs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyUserLog companyUserLog = db.CompanyUserLogs.Find(id);
            if (companyUserLog == null)
            {
                return HttpNotFound();
            }
            return View(companyUserLog);
        }

        // POST: CompanyUserLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyUserLog companyUserLog)
        {
            string filename = Path.GetFileNameWithoutExtension(companyUserLog.C_PhotoFile.FileName);
            string extension = Path.GetExtension(companyUserLog.C_PhotoFile.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            companyUserLog.C_Photos = "~/Photos/CompanyUsers/" + filename;

            filename = Path.Combine(Server.MapPath("~/Photos/CompanyUsers/"), filename);
            companyUserLog.C_PhotoFile.SaveAs(filename);


            db.Database.ExecuteSqlCommand("UPDATE CompanyUserLog SET " +
                "C_Name = '"        + companyUserLog.C_Name         + "' , C_Photos = '"        + companyUserLog.C_Photos       + "' , " +
                "C_Email = '"       + companyUserLog.C_Email        + "' , C_Passkey = '"       + companyUserLog.C_Passkey      + "' , " +
                "DateEstd = '"      + companyUserLog.DateEstd       + "' , C_ContactNo = '"     + companyUserLog.C_ContactNo    + "' , " +
                "C_CountryName = '" + companyUserLog.C_CountryName  + "' WHERE C_UserID = '"    + companyUserLog.C_UserID       + "'");




            /*if (ModelState.IsValid)
            {
                db.Entry(companyUserLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ModelState.Clear();*/
            return RedirectToAction("Dashboard");
        }









        // GET: CompanyUserLogs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyUserLog companyUserLog = db.CompanyUserLogs.Find(id);


            var res = db.Database.SqlQuery<CargoFlight>("Select * from CargoFlight where C_UserID = '" + companyUserLog.C_UserID + "' ;").ToList();
            List<CargoFlight> bookedFlights = new List<CargoFlight>();
            List<FlightInfo> bookedFlightInfo = new List<FlightInfo>();

            foreach (CargoFlight x in res)
            {
                var dt = db.Database.SqlQuery<FlightInfo>("Select * From  FlightInfo  Where FlightInfo.DepartureDate < (Select GETDATE()) AND FlightID = '" + x.FlightID + "'").FirstOrDefault();

                if (dt != null)
                {
                    bookedFlights.Add(x);
                    bookedFlightInfo.Add(dt);
                }

            }
            ViewBag.bookedCargoFlights = bookedFlights;
            ViewBag.bookedCargoFlightInfo = bookedFlightInfo;




            if (companyUserLog == null)
            {
                return HttpNotFound();
            }
            return View(companyUserLog);
        }

















        // GET: CompanyUserLogs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyUserLog companyUserLog = db.CompanyUserLogs.Find(id);
            if (companyUserLog == null)
            {
                return HttpNotFound();
            }
            return View(companyUserLog);
        }

        // POST: CompanyUserLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CompanyUserLog companyUserLog = db.CompanyUserLogs.Find(id);
            db.CompanyUserLogs.Remove(companyUserLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }














        // GET: CompanyUserLogs/Dashboard
        public ActionResult Dashboard()
        {
            string Email = Session["User_Email"].ToString();
            var userdashboard = db.CompanyUserLogs.Where(x => x.C_Email.Equals(Email)).FirstOrDefault();
            return View(userdashboard);
        }















        public ActionResult SearchForFlight(string id)
        {
            var res = db.Database.SqlQuery<string>("Select DISTINCT From1 from FlightInfo Where FlightInfo.FlightType = 'Cargo'").ToList();
            var mes = db.Database.SqlQuery<string>("Select DISTINCT To1 from FlightInfo Where FlightInfo.FlightType = 'Cargo'").ToList();
            ViewBag.src = res;
            ViewBag.des = mes;
            return View();
        }


        [HttpPost]
        public ActionResult SearchResult(SearchFlight sf)
        {
            var res = db.Database.SqlQuery<FlightInfo>("Select * from FlightInfo where From1 = '" + sf.Source + "' and To1 = '" + sf.Destination + "' and ClassType = 'Cargo'").ToList();
            List<FlightInfo> flightList = new List<FlightInfo>();
            foreach (FlightInfo x in res)
            {
                if (x.DepartureDate.ToString("dd-MM-yyyy").Equals(sf.FromDate.ToString("dd-MM-yyyy")))
                {
                    flightList.Add(x);
                }
            }
            ViewBag.flightList = flightList;
            return View();
        }


        [HttpGet]
        public ActionResult Booking(string flightId)
        {
            var seatList = new List<string>() {"A1","A2","A3","A4","A5","B1","B2","B3","B4","B5",
                                               "C1","C2","C3","C4","C5","D1","D2","D3","D4","D5",
                                               "E1","E2","E3","E4","E5","F1","F2","F3","F4","F5",
                                               "G1","G2","G3","G4","G5","H1","H2","H3","H4","H5"};
            var res = db.Database.SqlQuery<FlightInfo>("Select * from FlightInfo where FlightID = '" + flightId + "' ;").ToList();
            ViewBag.flightDetails = res;
            ViewBag.selectedFlightId = flightId;
            TempData["flightId"] = flightId;
            ViewBag.seatList = seatList;
            return View();
        }











        [HttpPost]
        public ActionResult Checkout(FormCollection form)
        {
            if (Session["User_Email"].ToString() == "")
            {
                ViewBag.Error = "You have not logged in yet!";
                return RedirectToAction("Login", "UserTypes", new { msg = "You Have not logged in yet!" });
            }

            string Email = SessionOfUserEmail = SessionOfUserID = Session["User_Email"].ToString(), userId, firstname, lastname;

            var usr = db.Database.SqlQuery<CompanyUserLog>("Select * from CompanyUserLog where C_Email = '" + Email + "' ;").ToList();
            ViewBag.username = usr[0].C_Name;
            ViewBag.useremail = usr[0].C_Email;
            userId = usr[0].C_UserID.ToString();


            ViewBag.flightId = TempData["flightId"];
            TempData.Keep("flightId");

            SessionOfUserID = userId;
            TempData["userId"] = SessionOfUserID;
            TempData.Keep("userId");

            TempData["totalFareForDb"] = form["totalFareForDb"];
            TempData.Keep("totalFareForDb");

            TempData["selectedSeats"] = form["selectedSeats"];
            TempData.Keep("selectedSeats");

            TempData["noOfSeats"] = form["noOfSeats"];
            TempData.Keep("noOfSeats");

            ViewBag.userId = userId;
            ViewBag.bookedSeats = form["selectedSeats"];
            ViewBag.noOfSeats = form["noOfSeats"];


            var productName = TempData["flightId"];
            var price = form["totalFareForDb"];

            // var baseUrl = Request.Url.Scheme + "://" + Request.UserHostName;
            var baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
            // CREATING LIST OF POST DATA
            NameValueCollection PostData = new NameValueCollection();

            PostData.Add("total_amount", $"{price}");
            PostData.Add("tran_id", "TESTASPNET1234");
            PostData.Add("success_url", baseUrl + "/CompanyUserLogs/CheckoutConfirmation");
            PostData.Add("fail_url", baseUrl + "/CompanyUserLogs/CheckoutFail");
            PostData.Add("cancel_url", baseUrl + "/CompanyUserLogs/CheckoutCancel");

            PostData.Add("version", "3.00");
            PostData.Add("cus_name", "ABC XY");
            PostData.Add("cus_email", "abc.xyz@mail.co");
            PostData.Add("cus_add1", "Address Line On");
            PostData.Add("cus_add2", "Address Line Tw");
            PostData.Add("cus_city", "City Nam");
            PostData.Add("cus_state", "State Nam");
            PostData.Add("cus_postcode", "Post Cod");
            PostData.Add("cus_country", "Countr");
            PostData.Add("cus_phone", "0111111111");
            PostData.Add("cus_fax", "0171111111");
            PostData.Add("ship_name", "Easy fly.com");
            PostData.Add("ship_add1", "Address Line On");
            PostData.Add("ship_add2", "Address Line Tw");
            PostData.Add("ship_city", "City Nam");
            PostData.Add("ship_state", "State Nam");
            PostData.Add("ship_postcode", "Post Cod");
            PostData.Add("ship_country", "Countr");
            PostData.Add("value_a", "ref00");
            PostData.Add("value_b", "ref00");
            PostData.Add("value_c", "ref00");
            PostData.Add("value_d", "ref00");
            PostData.Add("shipping_method", "NO");
            PostData.Add("num_of_item", "1");
            PostData.Add("product_name", $"{productName}");
            PostData.Add("product_profile", "general");
            PostData.Add("product_category", "Demo");

            //we can get from email notificaton
            var storeId = "aust614a65d74d901";
            var storePassword = "aust614a65d74d901@ssl";
            var isSandboxMood = true;

            SSLCommerzGatewayProcessor sslcz = new SSLCommerzGatewayProcessor(storeId, storePassword, isSandboxMood);

            string response = sslcz.InitiateTransaction(PostData);

            return Redirect(response);

            // return View();
        }





        public ActionResult CheckoutConfirmation()
        {
            if (!(!String.IsNullOrEmpty(Request.Form["status"]) && Request.Form["status"] == "VALID"))
            {
                ViewBag.SuccessInfo = "There some error while processing your payment. Please try again.";
                return View();
            }

            string TrxID = Request.Form["tran_id"];
            // AMOUNT and Currency FROM DB FOR THIS TRANSACTION
            string amount = price;
            string currency = "BDT";

            var storeId = "aust614a65d74d901";
            var storePassword = "aust614a65d74d901@ssl";

            SSLCommerzGatewayProcessor sslcz = new SSLCommerzGatewayProcessor(storeId, storePassword, true);
            //var resonse = sslcz.OrderValidate(TrxID, amount, currency, Request);
            //  var successInfo = $"Validation Response: {resonse}";

            ViewBag.SuccessInfo = "Payment Complete!";

            return RedirectToAction("ProceedToBooking");
        }





        public ActionResult ProceedToBooking()
        {
            string C_UserID = SessionOfUserID = TempData["userId"].ToString(), userId, firstname, lastname, Email;


            var usr = db.Database.SqlQuery<CompanyUserLog>("Select * from CompanyUserLog where C_UserID = '" + C_UserID + "'").ToList();
            ViewBag.username = usr[0].C_Name;
            ViewBag.useremail = usr[0].C_Email;
            userId = C_UserID;


            /*var UID = (from S in db.SingleUserLogs where S.S_UserID.Equals(S_UserID) select S.S_Email).FirstOrDefault();
            SessionOfUserEmail  =  Email = UID;

            var FN = (from S in db.SingleUserLogs where S.S_UserID.Equals(S_UserID) select S.FirstName).FirstOrDefault();
            firstname = FN;

            var LN = (from S in db.SingleUserLogs where S.S_UserID.Equals(S_UserID) select S.LastName).FirstOrDefault();
            lastname = LN;

            
            ViewBag.flightId = TempData["flightId"];      
            ViewBag.username = firstname + lastname;
            ViewBag.useremail = Email;*/



            ViewBag.flightId = TempData["flightId"];
            TempData.Keep("flightId");


            TempData["userId"] = userId;
            TempData.Keep("userId");


            ViewBag.totalFlightFare = TempData["totalFareForDb"];
            TempData.Keep("totalFareForDb");


            ViewBag.userId = userId;


            ViewBag.bookedSeats = TempData["selectedSeats"];
            TempData.Keep("selectedSeats");


            ViewBag.noOfSeats = TempData["noOfSeats"];
            TempData.Keep("noOfSeats");



            var res = db.Database.SqlQuery<FlightInfo>("Select * from FlightInfo where FlightID = '" + TempData["flightId"] + "' ;").ToList();
            ViewBag.flighDetails = res;

            int curNoOfSeats = (int)res[0].AvailableSeats;
            curNoOfSeats -= Convert.ToInt32(ViewBag.noOfSeats);

            string curBookedSeats = (string)res[0].BookedSeats + ViewBag.bookedSeats;

            int noOfRowInserted = db.Database.ExecuteSqlCommand(

                "INSERT INTO CargoFlight( FlightID,C_UserID,ProductType,ProductDimension,ProductWeight,ProductUnit,TotalCargoFare) " +
                "VALUES('" + TempData["flightId"] + "','" + userId + "', 'Solid' , '5' ,'" + ViewBag.noOfSeats + "','" + ViewBag.bookedSeats + "' , " + ViewBag.totalFlightFare + ")");

            
            int noOfRowUpdated = db.Database.ExecuteSqlCommand(
                "UPDATE FlightInfo SET AvailableSeats = " + curNoOfSeats + ", BookedSeats = '" + curBookedSeats + "'  WHERE FlightID = '" + TempData["flightId"] + "'");

            ViewBag.msg = "Something Went Wrong";

            if (noOfRowInserted > 0 && noOfRowUpdated > 0)
            {
                ViewBag.msg = "Booking Successful!!";
            }

            return View();
        }












        [HttpGet]
        public ActionResult CancelBooking(string id)
        {
            CompanyUserLog companyUserLog = db.CompanyUserLogs.Find(id);

            string C_UserID = companyUserLog.C_UserID.ToString();

            ViewBag.user = TempData["userId"] = C_UserID;
            string user = C_UserID;
            TempData.Keep();

            List<CargoFlight> bookedFlights = new List<CargoFlight>();
            List<string> depList = new List<string>();
            List<string> arrList = new List<string>();
            List<string> depDate = new List<string>();
            List<string> depTime = new List<string>();
            List<string> classType = new List<string>();

            var res = db.Database.SqlQuery<CargoFlight>("Select * from CargoFlight where C_UserID = '" + user + "' ;").ToList();

            foreach (CargoFlight x in res)
            {
                var dt = db.Database.SqlQuery<FlightInfo>("Select * From  FlightInfo  Where FlightInfo.DepartureDate > (Select GETDATE()) AND FlightID = '" + x.FlightID + "'").FirstOrDefault();

                /*string fdate = dt[0].DepartureDate.ToString("ddMMyyyy");
                string ftime = dt[0].DepartureDate.ToString("HHmmss");
                string curdate = System.DateTime.Today.ToString("ddMMyyyy");
                string curtime = System.DateTime.Today.ToString("HHmmss");
                
                if((fdate.CompareTo(curdate) == 1 || fdate.CompareTo(curdate) == 0) && ftime.CompareTo(curtime) == 1)
                {
                    bookedFlights.Add(x);
                    depList.Add(dt[0].From1);
                    arrList.Add(dt[0].To1);
                    depDate.Add(dt[0].DepartureDate.ToString("D"));
                    depTime.Add(dt[0].DepartureDate.ToString("t"));
                    classType.Add(dt[0].ClassType);
                }*/
                if (dt != null)
                {
                    bookedFlights.Add(x);
                    depList.Add(dt.From1);
                    arrList.Add(dt.To1);
                    depDate.Add(dt.DepartureDate.ToString("D"));
                    depTime.Add(dt.DepartureDate.ToString("t"));
                    classType.Add(dt.ClassType);
                }

            }


            ViewBag.bookedFlights = bookedFlights;
            ViewBag.depList = depList;
            ViewBag.arrList = arrList;
            ViewBag.depDate = depDate;
            ViewBag.depTime = depTime;
            ViewBag.classType = classType;


            return View();
        }




        [HttpGet]
        public ActionResult ConfirmDelete(string flightID, string bookedSeats)
        {

            string userId = TempData["userId"].ToString();

            string s = bookedSeats;
            var res = db.Database.SqlQuery<FlightInfo>("Select * from FlightInfo where FlightID = '" + flightID + "'").ToList();
            int noOfSeat = (int)res[0].AvailableSeats;
            string booked = res[0].BookedSeats+"5";
            
            string newBooked = "";
            noOfSeat += bookedSeats.Length / 2;
            
            int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update FlightInfo " +
               "set AvailableSeats = " + noOfSeat + ", BookedSeats = '" + newBooked + "'  where FlightID = '" + flightID + "'");
            
            int noOfRowDeleted = db.Database.ExecuteSqlCommand("DELETE FROM CargoFlight WHERE FlightID='" + flightID + "' AND C_UserID = '" + TempData["userId"] + "';");
            ViewBag.msg = "Something Wrong";
            
            if (noOfRowDeleted > 0 && noOfRowUpdated > 0)
            {
                ViewBag.msg = "Flight Cancelled Successfully!!!";
            }

            return View();
        }













        // GET: CompanyUserLogs
        public ActionResult Index()
        {
            /*adminid = Session["AdminID"].ToString();
            adminpassword = Session["AdminPassword"].ToString();

            if (adminid == "" || adminpassword == "")
            {
                return RedirectToAction("AdminLogin");
            }*/

            var TotalCompanyQuery = (from C in db.CompanyUserLogs select C.C_UserID).Count();

            var TotalCargosQuery = (from C in db.CargoFlights select C.FlightID).Count();


            String TotalNextCargosQuery = ("Select COUNT(*) From FlightInfo Inner Join CargoFlight On FlightInfo.FlightID = CargoFlight.FlightID Where DepartureDate > ( Select GETDATE() )");
            String TotalNextCargosQuery1 = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalNextCargosQuery, connection);
                    connection.Open();
                    TotalNextCargosQuery1 = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalNextCargosQuery1 = e.ToString();
            }

            ViewBag.TotalCompanies = TotalCompanyQuery;
            ViewBag.TotalCargos = TotalCargosQuery;
            ViewBag.TotalNextCargos = TotalNextCargosQuery1;

            return View(db.CompanyUserLogs.ToList());
        }

                

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
