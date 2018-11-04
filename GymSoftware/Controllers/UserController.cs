﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymSoftware;
using GymSoftware.Models;
using Service;
using CommonUtility;

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
            var CustList = _service.GetAllUser("", "");
            return View(CustList);
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
    }
}