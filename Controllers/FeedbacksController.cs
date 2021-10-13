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
    public class FeedbacksController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();



        public ActionResult Output(Feedback feedback)
        {
            string Email = Session["User_Email"].ToString();

            ViewBag.d = db.Database.SqlQuery<SingleUserLog>("SELECT * FROM SingleUserLog where S_Email = '" + Email + "'").ToList();


            if (feedback.S_UserID != null)
            {
                db.Database.ExecuteSqlCommand("Insert Into Feedback(FlightID,S_UserID,Feedback1,FlightRating) " +
                "Values( '" + feedback.FlightID + "' , '" + ViewBag.d[0].S_UserID + "' , '" + feedback.Feedback1 + "' , '" + feedback.FlightRating + "' )");
            }

            var res = db.Database.SqlQuery<Feedback>("SELECT TOP 5 * FROM Feedback ORDER BY S_UserID DESC").ToList();

            return View(res);
        }






        // GET: Feedbacks
        public ActionResult Index()
        {
            var feedbacks = db.Feedbacks.Include(f => f.FlightInfo).Include(f => f.SingleUserLog);
            return View(feedbacks.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        {
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "FlightID");
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_UserID");
            return View();
        }

        // POST: Feedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "FlightID", feedback.FlightID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_UserID", feedback.S_UserID);
            return View(feedback);
        }

        // GET: Feedbacks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "FlightID", feedback.FlightID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_UserID", feedback.S_UserID);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FlightID = new SelectList(db.FlightInfoes, "FlightID", "FlightID", feedback.FlightID);
            ViewBag.S_UserID = new SelectList(db.SingleUserLogs, "S_UserID", "S_UserID", feedback.S_UserID);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Feedback feedback = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(feedback);
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
