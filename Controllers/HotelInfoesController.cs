using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyFly.Models;

namespace EasyFly.Controllers
{
    public class HotelInfoesController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();

        // GET: HotelInfoes
        public ActionResult Index()
        {
            return View(db.HotelInfoes.ToList());
        }






        // GET: HotelInfoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelInfo hotelInfo = db.HotelInfoes.Find(id);
            if (hotelInfo == null)
            {
                return HttpNotFound();
            }
            return View(hotelInfo);
        }







        // GET: HotelInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HotelInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HotelInfo hotelInfo)
        {
            string filename = Path.GetFileNameWithoutExtension(hotelInfo.H_PhotoFile.FileName);
            string extension = Path.GetExtension(hotelInfo.H_PhotoFile.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            hotelInfo.HotelPhotos = "~/Photos/Hotels/" + filename;

            filename = Path.Combine(Server.MapPath("~/Photos/Hotels/"), filename);
            hotelInfo.H_PhotoFile.SaveAs(filename);


            if (ModelState.IsValid)
            {
                db.HotelInfoes.Add(hotelInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotelInfo);
        }









        // GET: HotelInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelInfo hotelInfo = db.HotelInfoes.Find(id);
            if (hotelInfo == null)
            {
                return HttpNotFound();
            }
            return View(hotelInfo);
        }

        // POST: HotelInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HotelInfo hotelInfo)
        {
            string filename = Path.GetFileNameWithoutExtension(hotelInfo.H_PhotoFile.FileName);
            string extension = Path.GetExtension(hotelInfo.H_PhotoFile.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            hotelInfo.HotelPhotos = "~/Photos/Hotels/" + filename;

            filename = Path.Combine(Server.MapPath("~/Photos/Hotels/"), filename);
            hotelInfo.H_PhotoFile.SaveAs(filename);


            db.Database.ExecuteSqlCommand("UPDATE HotelInfo SET " +
                "HotelName = '"     + hotelInfo.HotelName   + "' , HotelAddress = '"    + hotelInfo.HotelAddress        + "' , " +
                "HotelMail = '"     + hotelInfo.HotelMail   + "' , BusinessCapacity =  "+ hotelInfo.BusinessCapacity    + "  , " +
                "HotelPhotos = '"   + hotelInfo.HotelPhotos + "' , EconomyCapacity =   "+ hotelInfo.EconomyCapacity     + "  , " +
                "EcoRent =  "       + hotelInfo.EcoRent     + "  , BusiRent =  "        + hotelInfo.BusiRent            + "  , " +
                "ContactNo = '"     + hotelInfo.ContactNo   + "' WHERE HotelID = '"     + hotelInfo.HotelID + "'");

            /*if (ModelState.IsValid)
            {
                db.Entry(hotelInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotelInfo);*/

            return RedirectToAction("Index");
        }








        // GET: HotelInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelInfo hotelInfo = db.HotelInfoes.Find(id);
            if (hotelInfo == null)
            {
                return HttpNotFound();
            }
            return View(hotelInfo);
        }

        // POST: HotelInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HotelInfo hotelInfo = db.HotelInfoes.Find(id);
            db.HotelInfoes.Remove(hotelInfo);
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
