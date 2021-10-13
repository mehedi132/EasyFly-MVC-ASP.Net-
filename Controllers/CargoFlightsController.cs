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
    public class CargoFlightsController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();
        string adminid = "", adminpassword = "";

        // GET: CargoFlights
        public ActionResult Index()
        {
            var cargoFlights = db.CargoFlights.Include(c => c.CompanyUserLog).Include(c => c.FlightInfo);
            return View(cargoFlights.ToList());
        }

        // GET: CargoFlights/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoFlight cargoFlight = db.CargoFlights.Find(id);
            if (cargoFlight == null)
            {
                return HttpNotFound();
            }
            return View(cargoFlight);
        }

        // GET: CargoFlights/Create
        public ActionResult Create()
        {
            ViewBag.C_UserID = new SelectList(db.CompanyUserLogs, "C_UserID", "C_Photos");
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "AirCraftID");
            return View();
        }

        // POST: CargoFlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CargoFlight cargoFlight)
        {
            if (ModelState.IsValid)
            {
                db.CargoFlights.Add(cargoFlight);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_UserID = new SelectList(db.CompanyUserLogs, "C_UserID", "C_Photos", cargoFlight.C_UserID);
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "AirCraftID", cargoFlight.FlightID);
            return View(cargoFlight);
        }

        // GET: CargoFlights/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoFlight cargoFlight = db.CargoFlights.Find(id);
            if (cargoFlight == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_UserID = new SelectList(db.CompanyUserLogs, "C_UserID", "C_Photos", cargoFlight.C_UserID);
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "AirCraftID", cargoFlight.FlightID);
            return View(cargoFlight);
        }

        // POST: CargoFlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CargoFlight cargoFlight)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cargoFlight).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_UserID = new SelectList(db.CompanyUserLogs, "C_UserID", "C_Photos", cargoFlight.C_UserID);
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "AirCraftID", cargoFlight.FlightID);
            return View(cargoFlight);
        }

        // GET: CargoFlights/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CargoFlight cargoFlight = db.CargoFlights.Find(id);
            if (cargoFlight == null)
            {
                return HttpNotFound();
            }
            return View(cargoFlight);
        }

        // POST: CargoFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CargoFlight cargoFlight = db.CargoFlights.Find(id);
            db.CargoFlights.Remove(cargoFlight);
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
