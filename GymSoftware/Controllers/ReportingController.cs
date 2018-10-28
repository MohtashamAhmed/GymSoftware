using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymSoftware.Controllers
{
    public class ReportingController : Controller
    {
        // GET: Reporting
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult GraphicalReports()
        {
            return View();
        }
    }
}