using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyFly.com.Models;

namespace EasyFly.com.Controllers
{
    public class CompanyLogsController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();


        // GET: CompanyLogs/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        // GET: CompanyLogs/Login
        [HttpPost]
        public ActionResult Login(UserLogin userlogin)
        {
            if (ModelState.IsValid)
            {
                var userloginquery = db.CompanyLogs.Where(u => u.C_Email.Equals(userlogin.UserEmail)
                                        && u.C_Passkey.Equals(userlogin.Passkey)).FirstOrDefault();

                if (userloginquery != null)
                {
                    Session["User_Email"] = userlogin.UserEmail;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.LoginFailed = "User not found or wrong credentials!";
                    return View();
                }

            }
            return View();
        }

        // GET: CompanyLogs/Dashboard
        public ActionResult Dashboard()
        {
            string Email = Session["User_Email"].ToString();
            var userdashboard = db.CompanyLogs.Where(x => x.C_Email.Equals(Email)).FirstOrDefault();
            return View(userdashboard);
        }


        // GET: CompanyLogs
        public ActionResult Index()
        {
            string adminid = Session["AdminID"].ToString();
            string adminpassword = Session["AdminPassword"].ToString();

            if (adminid == "" || adminpassword == "")
            {
                return RedirectToAction("AdminLogin");
            }

            var TotalCompanyQuery = (from C in db.CompanyLogs select C.C_UserID).Count();

            var TotalCargosQuery = (from C in db.CargoFlights select C.FlightID).Count();


            String TotalNextCargosQuery = ("Select COUNT(*) From FlightInfo Inner Join CargoFlight On FlightInfo.FlightID = CargoFlight.FlightID Where DepartureDate > ( Select GETDATE() )");
            String TotalNextCargosQuery1 = "";

            try
            {
                string ConString = @"data source=SAQLAIN\SQLEXPRESS;initial catalog=EasyFlycomDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
                using (SqlConnection connection = new SqlConnection(ConString))
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

            return View(db.CompanyLogs.ToList());
        }

        // GET: CompanyLogs/Details/5
        public ActionResult Details(string id)
        {
            string adminid = Session["AdminID"].ToString();
            string adminpassword = Session["AdminPassword"].ToString();

            if (adminid == "" || adminpassword == "")
            {
                return RedirectToAction("AdminLogin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLog companyLog = db.CompanyLogs.Find(id);
            if (companyLog == null)
            {
                return HttpNotFound();
            }
            return View(companyLog);
        }

        // GET: CompanyLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyLog companyLog)
        {
            Random rand = new Random();
            int UserID = rand.Next(0, 100000);
            companyLog.C_UserID = "C_" + UserID.ToString();
            if (ModelState.IsValid)
            {
                db.CompanyLogs.Add(companyLog);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return Content("SignUp Successful!");
            }

            //return View(singleUserLog);
            return Content("SignUp Unsuccessful!");
        }

        // GET: CompanyLogs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLog companyLog = db.CompanyLogs.Find(id);
            if (companyLog == null)
            {
                return HttpNotFound();
            }
            return View(companyLog);
        }

        // POST: CompanyLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanyLog companyLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyLog);
        }

        // GET: CompanyLogs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyLog companyLog = db.CompanyLogs.Find(id);
            if (companyLog == null)
            {
                return HttpNotFound();
            }
            return View(companyLog);
        }

        // POST: CompanyLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CompanyLog companyLog = db.CompanyLogs.Find(id);
            db.CompanyLogs.Remove(companyLog);
            db.SaveChanges();
            return RedirectToAction("Index");
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
