using EasyFly.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace EasyFly.Controllers
{
    public class AdminLoginsController : Controller
    {
        private EasyFlycomDatabaseEntities db = new EasyFlycomDatabaseEntities();
        string adminid = "", adminpassword = "";
        UserType abc = new UserType();


        public ActionResult Chat()
        {
            return View();
        }




        // GET: AdminLogins
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        // GET: AdminLogins
        [HttpPost]
        public ActionResult AdminLogin(AdminLogin adminlogin)
        {
            if (ModelState.IsValid)
            {
                var adminStatus = (adminlogin.AdminID.Equals("1") && adminlogin.Password.Equals("1"));
                if (adminStatus)
                {
                    Session["AdminID"] = adminlogin.AdminID;
                    Session["AdminPassword"] = adminlogin.Password;
                    Session["User_Name"] = "admin";
                    return RedirectToAction("AdminDashboard");
                }
                else
                {
                    ViewBag.LoginFailed = "Wrong Credentials!";
                    return View();
                }
            }
            return View();
        }



        public ActionResult AdminDashboard()
        {
            /*adminid = Session["AdminID"].ToString();
            adminpassword = Session["AdminPassword"].ToString();

            if (adminid == "" || adminpassword == "")
            {
                return RedirectToAction("AdminLogin");
            }*/

            var RecentPassengerBookedflightQuery = db.Database.SqlQuery<FlightInfo>("Select * From  FlightInfo  Where FlightInfo.DepartureDate > (Select GETDATE()) And BookedSeats != '' And FlightInfo.FlightType = 'Passenger'").ToList();
            var AircraftQuery                    = db.Database.SqlQuery<AircraftInfo>("Select * From AircraftInfo");


            List<FlightInfo> RecentPassengerBookedflightList = new List<FlightInfo>();

            foreach (FlightInfo x in RecentPassengerBookedflightQuery)
            {
                foreach (AircraftInfo y in AircraftQuery)
                {
                    if ( x.AirCraftID.Equals(y.AC_ID))
                    {
                        x.AvailableSeats = y.PassengerCapacity - x.AvailableSeats;
                        RecentPassengerBookedflightList.Add(x);
                    }
                }
            }


            var RecentCargoBookedflightQuery = db.Database.SqlQuery<FlightInfo>("Select * From  FlightInfo  Where FlightInfo.DepartureDate > (Select GETDATE()) And BookedSeats != '' And FlightInfo.FlightType = 'Cargo'").ToList();


            List<FlightInfo> RecentCargoBookedflightList = new List<FlightInfo>();

            foreach (FlightInfo x in RecentCargoBookedflightQuery)
            {
                foreach (AircraftInfo y in AircraftQuery)
                {
                    if (x.AirCraftID.Equals(y.AC_ID))
                    {
                        x.AvailableSeats = y.CargoCapacity - x.AvailableSeats;
                        RecentCargoBookedflightList.Add(x);
                    }
                }
            }






            var TotalFlightRevenueQuery = "Select SUM(PassengerFlight.TotalFlightFare) From PassengerFlight Inner Join FlightInfo On FlightInfo.FlightID = PassengerFlight.FlightID Where DepartureDate < ( Select GETDATE() )";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalFlightRevenueQuery, connection);

                    connection.Open();

                    TotalFlightRevenueQuery = cm.ExecuteScalar().ToString();
                }
            } catch (Exception e){
                TotalFlightRevenueQuery = "0";
            }





            var TotalFlightRevenueQuery1 = "Select SUM(CargoFlight.TotalCargoFare) From CargoFlight Inner Join FlightInfo On FlightInfo.FlightID = CargoFlight.FlightID Where DepartureDate < ( Select GETDATE() )";
            try
            {
                using (SqlConnection connection = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm = new SqlCommand(TotalFlightRevenueQuery1, connection);

                    connection.Open();

                    TotalFlightRevenueQuery1 = cm.ExecuteScalar().ToString();
                }
            }
            catch (Exception e)
            {
                TotalFlightRevenueQuery1 = "0";
            }





            var TotalFlightsThisMonthQuery = "Select COUNT(*) From FlightInfo Where DepartureDate Between (SELECT DATEADD(mm, DATEDIFF(mm, 0, GETDATE()), 0)) And (SELECT DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) + 1, 0)))";
            try
            {
                using (SqlConnection connection1 = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm1 = new SqlCommand(TotalFlightsThisMonthQuery, connection1);

                    connection1.Open();

                    TotalFlightsThisMonthQuery = cm1.ExecuteScalar().ToString();
                }
            }catch (Exception e){
                TotalFlightsThisMonthQuery = e.ToString();
            }





            var TotalFlightsOnThisDayQuery = "Select COUNT(*) From FlightInfo Where DepartureDate Between (Select DATEADD(HOUR, DATEDIFF(HOUR, 0, GETDATE()),0)) And (SELECT DATEADD(HOUR, DATEDIFF(HOUR, 0, GETDATE()),1))";
            try
            {
                using (SqlConnection connection2 = new SqlConnection(abc.ConString))
                {
                    SqlCommand cm2 = new SqlCommand(TotalFlightsOnThisDayQuery, connection2);

                    connection2.Open();

                    TotalFlightsOnThisDayQuery = cm2.ExecuteScalar().ToString();
                }                
            }catch (Exception e){
                TotalFlightsOnThisDayQuery = "N/L";
            }




            var TotalFlightsExpensesQuery = "";

            if (TotalFlightRevenueQuery1 == null)   TotalFlightRevenueQuery1 = "0";

            if (TotalFlightRevenueQuery == null)   TotalFlightRevenueQuery = "0";


            ViewBag.TotalFlightRevenue              = Convert.ToInt32(TotalFlightRevenueQuery1) + Convert.ToInt32(TotalFlightRevenueQuery);
            ViewBag.TotalFlightsThisMonth           = TotalFlightsThisMonthQuery;
            ViewBag.TotalFlightsOnThisDay           = TotalFlightsOnThisDayQuery;
            ViewBag.TotalFlightsExpenses            = TotalFlightsExpensesQuery;
            ViewBag.RecentPassengerBookedflightList = RecentPassengerBookedflightList;
            ViewBag.RecentCargoBookedflightList     = RecentCargoBookedflightList;
            
            return View();

        }

        public ActionResult AdminLogOut()
        {
            Session["AdminID"] = null;
            Session["AdminPassword"] = null;
            return RedirectToAction("Index","Home");
        }
    }
}