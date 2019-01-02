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
    public class UserController : BaseController
    {
        UserService _service = new UserService();
        TrainerService _Trservice = new TrainerService();

        #region Customer Registration

        public ActionResult UserRegistration()
        {
            CustomerRegistration Registration = new CustomerRegistration();
            BindDropDowns();
            return View(Registration);
        }

        [HttpPost]
        public ActionResult UserRegistration(CustomerRegistration Registration)
        {
            BindDropDowns();
            if (!ModelState.IsValid)
                return View(Registration);

            string res = _service.CustomerRegistration(Registration);
            return RedirectToAction("DashboardDetails", "User", new { msg = res });
        }

        public void BindDropDowns()
        {
            var membershipdropdown = _service.GetMembershipDetails();
            var Batchdropdown = _service.GetBatchDetails();
            ViewBag.Membership = new SelectList(membershipdropdown, "ID", "Name");
            ViewBag.Batchdetails = new SelectList(Batchdropdown, "ID", "BatchName");
        }

        [HttpPost]
        public ActionResult BindPrice(int memid)
        {
            var PackagePrice = _service.GetMembershipDetails();
            int? Price = PackagePrice.Where(x => x.ID == memid).Select(x => x.Price).FirstOrDefault();
            return Json(new { price = Price, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult CheckMobile(string Mobile)
        {
            var data = _Trservice.CheckMobile(Mobile, "user");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Dashboard 

        public ActionResult DashboardDetails(string msg = "")
        {
            var Dashboard = _service.DashboardBoxCount();
            ViewBag.cmessage = msg;
            return View(Dashboard);
        }

        [HttpPost]
        public JsonResult dsdata()
        {
            var Dashboard = _service.DashboardGraph();
            return Json(Dashboard.ToArray(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult GetAllCustomers(string msg = "")
        {
            CustomerRegistration Model = new CustomerRegistration();
            Model.UsersList = _service.GetAllUser("", "");
            BindDropDowns();
            ViewBag.renewmsg = msg;
            return View(Model);
        }

        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = _service.GetAllUser("", "");
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Customer Details.xls");
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

        public ActionResult Receipts()
        {
            return View(_service.Receipts());
        }

        public JsonResult GetCustHistory(int custID)
        {
            var res = _service.GetCustomerHistory(custID);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reminders()
        {
            return View(_service.GetEmailList());
        }

        public ActionResult MyProfile()
        {
            return View();
        }

        public ActionResult FestiveOffers()
        {
            DisplayOffers Offer = new DisplayOffers();
            Offer.Offerlist = _service.GetMembershipDetails();
            return View(Offer);
        }
        [HttpPost]
        public ActionResult FestiveOffers(MembershipDetails Add)
        {
            if (!ModelState.IsValid)
                return View(Add);

            ViewBag.Message = _service.AddOffer(Add);
            ModelState.Clear();
            return View(Add);
        }

        public JsonResult UpdateOffer(int ID,bool isactive)
        {
            var res = _service.UpdateOffer(ID,isactive);
            return Json(res, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public JsonResult RenewAccount(CustomerRegistration model)
        {
            string res = _service.CustomerRegistration(model);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

    }
}