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
    public class CustomerSupportsController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();
        string adminid = "", adminpassword = "";

        // GET: CustomerSupports
        public ActionResult Index()
        {
            var customerSupports = db.CustomerSupports.Include(c => c.CompanyUserLog).Include(c => c.SingleUserLog);
            return View(customerSupports.ToList());
        }

        // GET: CustomerSupports/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSupport customerSupport = db.CustomerSupports.Find(id);
            if (customerSupport == null)
            {
                return HttpNotFound();
            }
            return View(customerSupport);
        }

        // GET: CustomerSupports/Create
        public ActionResult Create()
        {
            ViewBag.C_UserID = new SelectList(db.CompanyUserLogs, "C_UserID", "C_Photos");
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email");
            return View();
        }

        // POST: CustomerSupports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerSupport customerSupport)
        {
            if (ModelState.IsValid)
            {
                db.CustomerSupports.Add(customerSupport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.C_UserID = new SelectList(db.CompanyUserLogs, "C_UserID", "C_Photos", customerSupport.C_UserID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", customerSupport.S_UserID);
            return View(customerSupport);
        }

        // GET: CustomerSupports/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSupport customerSupport = db.CustomerSupports.Find(id);
            if (customerSupport == null)
            {
                return HttpNotFound();
            }
            ViewBag.C_UserID = new SelectList(db.CompanyUserLogs, "C_UserID", "C_Photos", customerSupport.C_UserID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", customerSupport.S_UserID);
            return View(customerSupport);
        }

        // POST: CustomerSupports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerSupport customerSupport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerSupport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.C_UserID = new SelectList(db.CompanyUserLogs, "C_UserID", "C_Photos", customerSupport.C_UserID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_Email", customerSupport.S_UserID);
            return View(customerSupport);
        }

        // GET: CustomerSupports/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSupport customerSupport = db.CustomerSupports.Find(id);
            if (customerSupport == null)
            {
                return HttpNotFound();
            }
            return View(customerSupport);
        }

        // POST: CustomerSupports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CustomerSupport customerSupport = db.CustomerSupports.Find(id);
            db.CustomerSupports.Remove(customerSupport);
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
