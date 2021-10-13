using EasyFly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyFly.Controllers
{
    public class HomeController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();
        public ActionResult Index()
        {
            Session["User_Email"] = "";
            var res = db.Database.SqlQuery<string>("Select DISTINCT From1 from FlightInfo Where FlightInfo.FlightType = 'Passenger'").ToList();
            var mes = db.Database.SqlQuery<string>("Select DISTINCT To1 from FlightInfo Where FlightInfo.FlightType = 'Passenger'").ToList();
            ViewBag.src = res;
            ViewBag.des = mes;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
