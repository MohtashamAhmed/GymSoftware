using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymSoftware;
using GymSoftware.Models;

namespace GymSoftware.Controllers
{
    public class UserController : Controller
    {
        public ActionResult UserRegistration()
        {
            return View();
        }
        public ActionResult GetAllCustomers()
        {
            List<CustomerVM> ListCusVM = new List<CustomerVM>();
           
            for (int i = 0; i < 10; i++)
            {
                CustomerVM cus = new CustomerVM();
                cus.Name = "Name" + i;
                cus.Mobile = "8143287477";
                cus.Source = "Social Media";
                cus.Payment = i.ToString();
                ListCusVM.Add(cus);
            }
            return View(ListCusVM);
        }
        
    }
}