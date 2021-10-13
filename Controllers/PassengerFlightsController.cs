using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyFly.Models;

namespace EasyFly.Controllers
{
    public class PassengerFlightsController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();

        // GET: PassengerFlights
        public ActionResult Index()
        {
            var passengerFlights = db.PassengerFlights.Include(p => p.FlightInfo).Include(p => p.HotelInfo).Include(p => p.SingleUserLog);
            return View(passengerFlights.ToList());
        }

        // GET: PassengerFlights/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerFlight passengerFlight = db.PassengerFlights.Find(id);
            if (passengerFlight == null)
            {
                return HttpNotFound();
            }
            return View(passengerFlight);
        }

        // GET: PassengerFlights/Create
        public ActionResult Create()
        {
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "AirCraftID");
            ViewBag.HotelID = new SelectList(db.HotelInfoes, "HotelID", "HotelName");
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email");
            return View();
        }

        // POST: PassengerFlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlightID,S_UserID,NoOfSeats,SeatNumbers,OfferID,HotelID,TotalHotelRent,TotalFlightFare")] PassengerFlight passengerFlight)
        {
            if (ModelState.IsValid)
            {
                db.PassengerFlights.Add(passengerFlight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "AirCraftID", passengerFlight.FlightID);
            ViewBag.HotelID = new SelectList(db.HotelInfoes, "HotelID", "HotelName", passengerFlight.HotelID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", passengerFlight.S_UserID);
            return View(passengerFlight);
        }

        // GET: PassengerFlights/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerFlight passengerFlight = db.PassengerFlights.Find(id);
            if (passengerFlight == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "AirCraftID", passengerFlight.FlightID);
            ViewBag.HotelID = new SelectList(db.HotelInfoes, "HotelID", "HotelName", passengerFlight.HotelID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", passengerFlight.S_UserID);
            return View(passengerFlight);
        }

        // POST: PassengerFlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlightID,S_UserID,NoOfSeats,SeatNumbers,OfferID,HotelID,TotalHotelRent,TotalFlightFare")] PassengerFlight passengerFlight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(passengerFlight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "AirCraftID", passengerFlight.FlightID);
            ViewBag.HotelID = new SelectList(db.HotelInfoes, "HotelID", "HotelName", passengerFlight.HotelID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", passengerFlight.S_UserID);
            return View(passengerFlight);
        }

        // GET: PassengerFlights/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerFlight passengerFlight = db.PassengerFlights.Find(id);
            if (passengerFlight == null)
            {
                return HttpNotFound();
            }
            return View(passengerFlight);
        }

        // POST: PassengerFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PassengerFlight passengerFlight = db.PassengerFlights.Find(id);
            db.PassengerFlights.Remove(passengerFlight);
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
