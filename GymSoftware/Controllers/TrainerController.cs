﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymSoftware.Controllers
{
    public class TrainerController : Controller
    {
        // GET: Trainer
        public ActionResult TrainerRegistration()
        {
            return View();
        }
        public ActionResult GetAllTrainer()
        {
            return View();
        }
    }
}