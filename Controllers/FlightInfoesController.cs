using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyFly.Models;

namespace EasyFly.Controllers
{
    public class FlightInfoesController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();
        UserType abc = new UserType();






        // GET: FlightInfoes
        public ActionResult Index()
        {
            /*string adminid = Session["AdminID"].ToString();
            string adminpassword = Session["AdminPassword"].ToString();

            if (adminid == "" || adminpassword == "")
            {
                return RedirectToAction("AdminLogin");
            }*/

            var TotalFlightsQuery           = (from F in db.FlightInfoes select F.FlightID).Count();

            var TotalPassengerFlightsQuery  = (from S in db.FlightInfoes where S.FlightType.Equals("Passenger") select S.FlightID).Count();

            var TotalCargoFlightsQuery      = (from C in db.FlightInfoes where C.FlightType.Equals("Cargo") select C.FlightID).Count();

            var TotalNextFlightsQuery       = ("Select COUNT(*) From FlightInfo Where DepartureDate > ( Select GETDATE() )");
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalNextFlightsQuery, connection);
                    connection.Open();
                    TotalNextFlightsQuery = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalNextFlightsQuery = e.ToString();
            }          


            ViewBag.TotalFlights            = TotalFlightsQuery;
            ViewBag.TotalPassengerFlights   = TotalPassengerFlightsQuery;
            ViewBag.TotalCargoFlights       = TotalCargoFlightsQuery;
            ViewBag.TotalNextFlights        = TotalNextFlightsQuery;

            var flightInfoes = db.FlightInfoes.Include(f => f.AircraftInfo).Include(f => f.Employee).Include(f => f.Employee1).Include(f => f.Employee2).Include(f => f.Employee3).Include(f => f.Employee4).Include(f => f.Employee5).Include(f => f.Employee6);
            return View(flightInfoes.ToList());
        }











        // GET: FlightInfoes/Details/5
        public ActionResult Details(string id)
        {
            /*string adminid = Session["AdminID"].ToString();
            string adminpassword = Session["AdminPassword"].ToString();

            if (adminid == "" || adminpassword == "")
            {
                return RedirectToAction("AdminLogin");
            }*/


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FlightInfo flightInfo = db.FlightInfoes.Find(id);          

            if (flightInfo == null)
            {
                return HttpNotFound();
            }


            var seatList = new List<string>() {"A1","A2","A3","A4","A5","B1","B2","B3","B4","B5",
                                               "C1","C2","C3","C4","C5","D1","D2","D3","D4","D5",
                                               "E1","E2","E3","E4","E5","F1","F2","F3","F4","F5",
                                               "G1","G2","G3","G4","G5","H1","H2","H3","H4","H5"};
            
            var res = db.Database.SqlQuery<FlightInfo>("Select * From FlightInfo Where FlightID = '" + flightInfo.FlightID + "'").ToList();
            var mes = db.Database.SqlQuery<Feedback>("Select * From Feedback Where FlightID = '" + flightInfo.FlightID + "'").ToList();
            var pas = db.Database.SqlQuery<SingleUserLog>("Select * From SingleUserLog");



            var kes = (from feed in db.Feedbacks join flight in db.FlightInfoes
                       on feed.FlightID equals flight.FlightID  
                       where flight.FlightID.Equals(flightInfo.FlightID) 
                       select new{
                            SingleUserID  = feed.S_UserID,
                            FeedbackText  = feed.Feedback1,
                            FlightID1  = flight.FlightID,
                            DepartCity = flight.From1,
                            ArriveCity = flight.To1,
                            DepartDate = flight.DepartureDate,
                            ArriveDate = flight.ArrivalDate,
                            AircraftID = flight.AirCraftID,
                            Flighttype = flight.FlightType                            
                        }).ToList();



            ViewBag.PassengerDetails = pas;
            ViewBag.flightFeedbacks  = mes;
            ViewBag.flightDetails    = res;
            ViewBag.selectedFlightId = flightInfo.FlightID;
            TempData["flightId"]     = flightInfo.FlightID;
            ViewBag.seatList         = seatList;

            return View(flightInfo);
        }










        // GET: FlightInfoes/Create
        public ActionResult Create()
        {
            FlightInfo City = new FlightInfo();

            City.CityList = new List<string>()
            {
                "Dhaka","Chattogram","Cox's Bazar","New York","Washington","Mumbai","Dubai","Sylhet","Jessore","Saydpur","London",
                "Paris","Berlin","Madrid","Manchester","Barisal","Delhi"
            };

            City.CityList.Sort();

            

            var FreeEmployeeQuery = db.Database.SqlQuery<FlightInfo>("Select * From  FlightInfo  Where FlightInfo.ArrivalDate < (Select GETDATE())").ToList();
            var EmployeeQuery = db.Database.SqlQuery<Employee>("Select * From Employee Where DutyStatus = 'Free'");            


            City.Pilot1List     = new List<Employee>();
            City.Pilot2List     = new List<Employee>();
            City.CabinCrew1List = new List<Employee>();
            City.CabinCrew2List = new List<Employee>();
            City.CabinCrew3List = new List<Employee>();
            City.Assistant1List = new List<Employee>();
            City.Assistant2List = new List<Employee>();


            foreach (FlightInfo x in FreeEmployeeQuery)
            {
                foreach (Employee y in EmployeeQuery)
                {
                    if (y.E_Designation.Equals("Pilot"))
                    {
                        if (x.Pilot1.Equals(y.EmployeeID))
                        {
                            City.Pilot1List.Add(y);
                        }
                        else if (x.Pilot2.Equals(y.EmployeeID))
                        {
                            City.Pilot2List.Add(y);
                        }
                    }
                    else if (y.E_Designation.Equals("Cabin Crew"))
                    {
                        if (x.CabinCrew1 != null && x.CabinCrew1.Equals(y.EmployeeID))
                        {
                            City.CabinCrew1List.Add(y);
                        }
                        else if (x.CabinCrew2 != null && x.CabinCrew2.Equals(y.EmployeeID))
                        {
                            City.CabinCrew2List.Add(y);
                        }
                        else if (x.CabinCrew3 != null && x.CabinCrew3.Equals(y.EmployeeID))
                        {
                            City.CabinCrew3List.Add(y);
                        }
                    }
                    else if (y.E_Designation.Equals("Assistant"))
                    {
                        if (x.Assistant1.Equals(y.EmployeeID))
                        {
                            City.Assistant1List.Add(y);
                        }
                        else if (x.Assistant2.Equals(y.EmployeeID))
                        {
                            City.Assistant2List.Add(y);
                        }
                    }
                }
            }




            ViewBag.CityList    = City.CityList;
            ViewBag.AirCraftID  = new SelectList(db.AircraftInfoes,   "AC_ID", "AC_Name");
            ViewBag.Pilot1      = new SelectList(City.Pilot1List,     "EmployeeID", "E_Name");
            ViewBag.Pilot2      = new SelectList(City.Pilot2List,     "EmployeeID", "E_Name");
            ViewBag.CabinCrew1  = new SelectList(City.CabinCrew1List, "EmployeeID", "E_Name");
            ViewBag.CabinCrew2  = new SelectList(City.CabinCrew2List, "EmployeeID", "E_Name");
            ViewBag.CabinCrew3  = new SelectList(City.CabinCrew3List, "EmployeeID", "E_Name");
            ViewBag.Assistant1  = new SelectList(City.Assistant1List, "EmployeeID", "E_Name");
            ViewBag.Assistant2  = new SelectList(City.Assistant2List, "EmployeeID", "E_Name");
            
            return View();
        }















        // POST: FlightInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlightInfo flightInfo)
        {
            flightInfo.BookedSeats = "";
            if (ModelState.IsValid)
            {
                db.FlightInfoes.Add(flightInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AirCraftID = new SelectList(db.AircraftInfoes, "AC_ID", "AC_Name", flightInfo.AirCraftID);
            ViewBag.Assistant1 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Assistant1);
            ViewBag.Assistant2 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Assistant2);
            ViewBag.CabinCrew1 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew1);
            ViewBag.CabinCrew2 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew2);
            ViewBag.CabinCrew3 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew3);
            ViewBag.Pilot1 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Pilot1);
            ViewBag.Pilot2 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Pilot2);
            return View(flightInfo);
        }

        // GET: FlightInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightInfo flightInfo = db.FlightInfoes.Find(id);
            
            if (flightInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.AirCraftID = new SelectList(db.AircraftInfoes, "AC_ID", "AC_Name", flightInfo.AirCraftID);
            ViewBag.Pilot1 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Pilot1);
            ViewBag.Pilot2 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Pilot2);
            ViewBag.CabinCrew1 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew1);
            ViewBag.CabinCrew2 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew2);
            ViewBag.CabinCrew3 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew3);
            ViewBag.Assistant1 = new SelectList(db.Employees, "EmployeeID", "E_Name" , flightInfo.Assistant1);
            ViewBag.Assistant2 = new SelectList(db.Employees, "EmployeeID", "E_Name" , flightInfo.Assistant2);            
             
            return View(flightInfo);
        }

        // POST: FlightInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FlightInfo flightInfo)
        {
            flightInfo.BookedSeats = "";
            
            /*if(flightInfo.FlightType == "Cargo")
            {
                flightInfo.CabinCrew1 = "";
                flightInfo.CabinCrew2 = "";
                flightInfo.CabinCrew3 = "";
            }*/


            if (ModelState.IsValid)
            {
                db.Entry(flightInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AirCraftID = new SelectList(db.AircraftInfoes, "AC_ID", "AC_Name", flightInfo.AirCraftID);
            ViewBag.Assistant1 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Assistant1);
            ViewBag.Assistant2 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Assistant2);
            ViewBag.CabinCrew1 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew1);
            ViewBag.CabinCrew2 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew2);
            ViewBag.CabinCrew3 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.CabinCrew3);
            ViewBag.Pilot1 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Pilot1);
            ViewBag.Pilot2 = new SelectList(db.Employees, "EmployeeID", "E_Name", flightInfo.Pilot2);

            return View(flightInfo);
        }

        // GET: FlightInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FlightInfo flightInfo = db.FlightInfoes.Find(id);
            if (flightInfo == null)
            {
                return HttpNotFound();
            }
            return View(flightInfo);
        }

        // POST: FlightInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FlightInfo flightInfo = db.FlightInfoes.Find(id);
            db.FlightInfoes.Remove(flightInfo);
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
