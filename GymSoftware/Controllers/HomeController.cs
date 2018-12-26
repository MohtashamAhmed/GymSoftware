using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymSoftware.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {    
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

        public ActionResult DashboardDetails()
        {
            return View();
        }

        public ActionResult UserRegistration()
        {
            return View();
        }

        public ActionResult TrainerRegistration()
        {
            return View();
        }

        public ActionResult GetAllCustomers()
        {
            return View();
        }

        public ActionResult GetAllTrainer()
        {
            return View();
        }

        public ActionResult Receipts()
        {
            return View();
        }

        public ActionResult Reminders()
        {
            return View();
        }


    }
}