using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymSoftware;
using GymSoftware.Models;
using Service;
using CommonUtility;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Web.Security;

namespace GymSoftware.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        UserService _service = new UserService();

        public ActionResult UserRegistration()
        {
            CustomerRegistration Registration = new CustomerRegistration();
            BindDropDowns();
            return View(Registration);
        }

        [HttpPost]
        public ActionResult UserRegistration(CustomerRegistration Registration)
        {
            if (!ModelState.IsValid)
                return View(Registration);

            string res = _service.CustomerRegistration(Registration);
            BindDropDowns();
            return RedirectToAction("DashboardDetails", "User", new { msg = res });           
        }

        public ActionResult GetAllCustomers()
        {
            CustomerRegistration Model = new CustomerRegistration();
            Model.UsersList = _service.GetAllUser("", "");
            return View(Model);
        }

        [HttpPost]
        public ActionResult BindPrice(int memid)
        {
            var PackagePrice = _service.GetMembershipDetails();
            int? Price = PackagePrice.Where(x => x.ID == memid).Select(x => x.Price).FirstOrDefault();
            return Json(new { price = Price, JsonRequestBehavior.AllowGet });
        }

        public void BindDropDowns()
        {
            var membershipdropdown = _service.GetMembershipDetails();
            var Batchdropdown = _service.GetBatchDetails();
            ViewBag.Membership = new SelectList(membershipdropdown, "ID", "Name");
            ViewBag.Batchdetails = new SelectList(Batchdropdown, "ID", "BatchName");
        }

        public ActionResult DashboardDetails(string msg="")
        {
            //var Dashboard = _service.DashboardDetails();
            ViewBag.cmessage = msg;
            return View();
        }

        public ActionResult Receipts()
        {
            var Receipts = _service.Receipts();
            return View(Receipts);
        }

        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = _service.GetAllUser("", "");
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=CustomerDetails.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return View("");

        }
    }
}