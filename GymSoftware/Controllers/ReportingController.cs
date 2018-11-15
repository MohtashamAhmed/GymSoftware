using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymSoftware.Controllers
{
    [Authorize]
    public class ReportingController : BaseController
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