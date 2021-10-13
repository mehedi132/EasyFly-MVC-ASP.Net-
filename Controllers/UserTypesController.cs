using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyFly.Controllers
{
    public class UserTypesController : Controller
    {
        // GET: UserTypes
        public ActionResult Login(string msg)
        {
            ViewBag.loginErrorMsg = msg;
            Session["User_Email"] = "";            
            return View();
        }
    }
}