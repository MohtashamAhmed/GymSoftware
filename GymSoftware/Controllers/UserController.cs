﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymSoftware;

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
            return View();
        }
        
    }
}