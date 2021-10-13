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
    public class AircraftInfoesController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();
        string adminid = "", adminpassword = "";
        UserType abc = new UserType();

        // GET: AircraftInfoes
        public ActionResult Index()
        {
            /*string adminid = Session["AdminID"].ToString();
            string adminpassword = Session["AdminPassword"].ToString();

            if (adminid == "" || adminpassword == "")
            {
                return RedirectToAction("AdminLogin");
            }*/


            var TotalAircraftQuery = "Select Count(*) From AircraftInfo";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalAircraftQuery, connection);

                    connection.Open();

                    TotalAircraftQuery = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalAircraftQuery = e.ToString();
            }




            var TotalPassengerAircraftQuery = "Select Count(*) From AircraftInfo Where AC_Type = 'Passenger'";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalPassengerAircraftQuery, connection);

                    connection.Open();

                    TotalPassengerAircraftQuery = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalPassengerAircraftQuery = e.ToString();
            }




            var TotalCargoAircraftQuery = "Select Count(*) From AircraftInfo Where AC_Type = 'Cargo'";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalCargoAircraftQuery, connection);

                    connection.Open();

                    TotalCargoAircraftQuery = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalCargoAircraftQuery = e.ToString();
            }




            var TotalAircraftsInOperationQuery = "Select COUNT(*) From FlightInfo Where DepartureDate< (Select GETDATE() )  And ArrivalDate > (Select GETDATE() )";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalAircraftsInOperationQuery, connection);

                    connection.Open();

                    TotalAircraftsInOperationQuery = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalAircraftsInOperationQuery = e.ToString();
            }





            ViewBag.TotalAircraft               = TotalAircraftQuery;
            ViewBag.TotalPassengerAircraft      = TotalPassengerAircraftQuery;
            ViewBag.TotalCargoAircraft          = TotalCargoAircraftQuery;
            ViewBag.TotalAircraftsInOperation   = TotalAircraftsInOperationQuery;


            return View(db.AircraftInfoes.ToList());
        }



        // GET: AircraftInfoes/Details/5
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
            AircraftInfo aircraftInfo = db.AircraftInfoes.Find(id);
            if (aircraftInfo == null)
            {
                return HttpNotFound();
            }
            return View(aircraftInfo);
        }

        // GET: AircraftInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AircraftInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AircraftInfo aircraftInfo)
        {

            if (ModelState.IsValid)
            {
                db.AircraftInfoes.Add(aircraftInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aircraftInfo);
        }

        // GET: AircraftInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AircraftInfo aircraftInfo = db.AircraftInfoes.Find(id);
            if (aircraftInfo == null)
            {
                return HttpNotFound();
            }
            return View(aircraftInfo);
        }

        // POST: AircraftInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AircraftInfo aircraftInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aircraftInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aircraftInfo);
        }

        // GET: AircraftInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AircraftInfo aircraftInfo = db.AircraftInfoes.Find(id);
            if (aircraftInfo == null)
            {
                return HttpNotFound();
            }
            return View(aircraftInfo);
        }

        // POST: AircraftInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AircraftInfo aircraftInfo = db.AircraftInfoes.Find(id);
            db.AircraftInfoes.Remove(aircraftInfo);
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
