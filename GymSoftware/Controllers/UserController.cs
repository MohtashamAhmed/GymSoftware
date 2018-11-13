﻿using System;
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

namespace GymSoftware.Controllers
{
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

            var result = _service.CustomerRegistration(Registration);
            BindDropDowns();
            return View(Registration);
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

        public ActionResult DashboardDetails()
        {
            //var Dashboard = _service.DashboardDetails();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login( UserModel UM)
        {
            var log = _service.Login(UM);
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
            gv.DataSource = _service.GetAllUser("","");
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
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