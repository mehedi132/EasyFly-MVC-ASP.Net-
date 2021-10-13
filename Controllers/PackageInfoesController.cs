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
    public class PackageInfoesController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();





        // GET: PackageInfoes
        public ActionResult Index()
        {
            return View(db.PackageInfoes.ToList());
        }




        // GET: PackageInfoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageInfo packageInfo = db.PackageInfoes.Find(id);
            if (packageInfo == null)
            {
                return HttpNotFound();
            }
            return View(packageInfo);
        }








        // GET: PackageInfoes/Create
        public ActionResult Create()
        {
            var HotelListQuery = db.Database.SqlQuery<HotelInfo>("Select * FROM HotelInfo").ToList();

            List<String> HotelList = new List<String>();

            foreach (var x in HotelListQuery)
            {
                HotelList.Add(x.HotelID.ToString());
            }

            ViewBag.HotelList = HotelList;


            return View();
        }

        // POST: PackageInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PackageInfo packageInfo)
        {
            var HotelListQuery = db.Database.SqlQuery<HotelInfo>("Select * From HotelInfo Where HotelID = '"+packageInfo.HotelName+"'").ToList();

            packageInfo.HotelName   = HotelListQuery[0].HotelName;
            packageInfo.HotelImage  = HotelListQuery[0].HotelPhotos;
            packageInfo.Destination = HotelListQuery[0].HotelAddress; 

            if (ModelState.IsValid)
            {
                db.PackageInfoes.Add(packageInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(packageInfo);
        }














        // GET: PackageInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var HotelListQuery = db.Database.SqlQuery<HotelInfo>("Select * FROM HotelInfo").ToList();

            List<String> HotelList = new List<String>();

            foreach (var x in HotelListQuery)
            {
                HotelList.Add(x.HotelName);
            }

            ViewBag.HotelList = HotelList;
            PackageInfo packageInfo = db.PackageInfoes.Find(id);
            if (packageInfo == null)
            {
                return HttpNotFound();
            }
            return View(packageInfo);
        }

        // POST: PackageInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PackageInfo packageInfo)
        {
            var HotelListQuery = db.Database.SqlQuery<HotelInfo>("Select * From HotelInfo Where HotelID = '" + packageInfo.HotelName + "'").ToList();

            packageInfo.HotelName = HotelListQuery[0].HotelName;
            packageInfo.HotelImage = HotelListQuery[0].HotelPhotos;
            packageInfo.Destination = HotelListQuery[0].HotelAddress;

            if (ModelState.IsValid)
            {
                db.Entry(packageInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(packageInfo);
        }











        // GET: PackageInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PackageInfo packageInfo = db.PackageInfoes.Find(id);
            if (packageInfo == null)
            {
                return HttpNotFound();
            }
            return View(packageInfo);
        }

        // POST: PackageInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PackageInfo packageInfo = db.PackageInfoes.Find(id);
            db.PackageInfoes.Remove(packageInfo);
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
