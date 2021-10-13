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
    public class BookedPackagesController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();

        // GET: BookedPackages
        public ActionResult Index()
        {
            var bookedPackages = db.BookedPackages.Include(b => b.PackageInfo).Include(b => b.SingleUserLog);
            return View(bookedPackages.ToList());
        }

        // GET: BookedPackages/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookedPackage bookedPackage = db.BookedPackages.Find(id);
            if (bookedPackage == null)
            {
                return HttpNotFound();
            }
            return View(bookedPackage);
        }

        // GET: BookedPackages/Create
        public ActionResult Create()
        {
            ViewBag.PackageID = new SelectList(db.PackageInfoes, "PackageID", "HotelName");
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email");
            return View();
        }

        // POST: BookedPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "S_UserID,PackageID,CheckInDate,NoOfPerson,TotalPrice")] BookedPackage bookedPackage)
        {
            if (ModelState.IsValid)
            {
                db.BookedPackages.Add(bookedPackage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PackageID = new SelectList(db.PackageInfoes, "PackageID", "HotelName", bookedPackage.PackageID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", bookedPackage.S_UserID);
            return View(bookedPackage);
        }

        // GET: BookedPackages/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookedPackage bookedPackage = db.BookedPackages.Find(id);
            if (bookedPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageID = new SelectList(db.PackageInfoes, "PackageID", "HotelName", bookedPackage.PackageID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", bookedPackage.S_UserID);
            return View(bookedPackage);
        }

        // POST: BookedPackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "S_UserID,PackageID,CheckInDate,NoOfPerson,TotalPrice")] BookedPackage bookedPackage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookedPackage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PackageID = new SelectList(db.PackageInfoes, "PackageID", "HotelName", bookedPackage.PackageID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", bookedPackage.S_UserID);
            return View(bookedPackage);
        }

        // GET: BookedPackages/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookedPackage bookedPackage = db.BookedPackages.Find(id);
            if (bookedPackage == null)
            {
                return HttpNotFound();
            }
            return View(bookedPackage);
        }

        // POST: BookedPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BookedPackage bookedPackage = db.BookedPackages.Find(id);
            db.BookedPackages.Remove(bookedPackage);
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
