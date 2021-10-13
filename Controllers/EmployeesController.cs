using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EasyFly.Models;

namespace EasyFly.Controllers
{
    public class EmployeesController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();
        UserType abc = new UserType();





        // GET: Employees
        public ActionResult Index()
        {
            var TotalEmployeeQuery = (from E in db.Employees select E.EmployeeID).Count();

            
            
            var TotalDutyEmployeesQuery = "Select COUNT(*) From FlightInfo Inner Join PassengerFlight On FlightInfo.FlightID = PassengerFlight.FlightID Where FlightInfo.DepartureDate < ( Select GETDATE() )";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalDutyEmployeesQuery, connection);
                    connection.Open();
                    TotalDutyEmployeesQuery = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalDutyEmployeesQuery = e.ToString();
            }





            var TotalDutyEmployeesQuery1 = "Select COUNT(*) From FlightInfo Inner Join CargoFlight On FlightInfo.FlightID = CargoFlight.FlightID Where FlightInfo.DepartureDate < ( Select GETDATE() )";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalDutyEmployeesQuery1, connection);
                    connection.Open();
                    TotalDutyEmployeesQuery1 = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalDutyEmployeesQuery1 = e.ToString();
            }









            var TotalNextDutyEmployeesQuery = "Select COUNT(*) From FlightInfo Inner Join CargoFlight On FlightInfo.FlightID = CargoFlight.FlightID Where FlightInfo.DepartureDate > ( Select GETDATE() )";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalNextDutyEmployeesQuery, connection);
                    connection.Open();
                    TotalNextDutyEmployeesQuery = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalNextDutyEmployeesQuery = e.ToString();
            }





            var TotalNextDutyEmployeesQuery1 = "Select COUNT(*) From FlightInfo Inner Join PassengerFlight On FlightInfo.FlightID = PassengerFlight.FlightID Where FlightInfo.DepartureDate > ( Select GETDATE() )";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalNextDutyEmployeesQuery1, connection);
                    connection.Open();
                    TotalNextDutyEmployeesQuery1 = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalNextDutyEmployeesQuery1 = e.ToString();
            }



            var TotalDutyFreeEmployeesQuery = "Select COUNT(*) From Employee Where DutyStatus = 'Free'";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalDutyFreeEmployeesQuery, connection);
                    connection.Open();
                    TotalDutyFreeEmployeesQuery = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalDutyFreeEmployeesQuery = e.ToString();
            }




            ViewBag.TotalEmployee = TotalEmployeeQuery;
            ViewBag.TotalDutyEmployees =  Convert.ToInt32(TotalDutyEmployeesQuery) * 7 + Convert.ToInt32(TotalDutyEmployeesQuery1) * 4; ;
            ViewBag.TotalNextDutyEmployees = Convert.ToInt32(TotalNextDutyEmployeesQuery1) * 7 + Convert.ToInt32(TotalNextDutyEmployeesQuery) * 4;
            ViewBag.TotalDutyFreeEmployees = TotalDutyFreeEmployeesQuery;
            return View(db.Employees.ToList());
        }






        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {               

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = db.Employees.Find(id);

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }






        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }





        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            string filename  = Path.GetFileNameWithoutExtension(employee.E_PhotoFile.FileName);
            string extension = Path.GetExtension(employee.E_PhotoFile.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            employee.E_Photos = "~/Photos/Employees/" + filename;

            filename = Path.Combine(Server.MapPath("~/Photos/Employees/"), filename);
            employee.E_PhotoFile.SaveAs(filename);


            employee.DutyStatus = "Free";

            Random rand = new Random();
            int UserID = rand.Next(0, 1000);
            employee.EmployeeID = employee.EmployeeID + UserID.ToString();



            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();

            return View(employee);
        }






        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }







        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            string filename = Path.GetFileNameWithoutExtension(employee.E_PhotoFile.FileName);
            string extension = Path.GetExtension(employee.E_PhotoFile.FileName);

            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            employee.E_Photos = "~/Photos/Employees/" + filename;

            filename = Path.Combine(Server.MapPath("~/Photos/Employees/"), filename);
            employee.E_PhotoFile.SaveAs(filename);


            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.Clear();
            return View(employee);
        }






        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }






        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
