using CommonUtility;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymSoftware.Controllers
{
    public class TrainerController : Controller
    {
        TrainerService _service = new TrainerService();
        public ActionResult TrainerRegistration()
        {
            TrainerModel Trainer = new TrainerModel();
            return View(Trainer);
        }

        [HttpPost]
        public ActionResult TrainerRegistration(TrainerModel Trainer)
        {
            string result = _service.TrainerRegistration(Trainer);
            return View(Trainer);
        }

        public ActionResult GetAllTrainer()
        {
            var Trainerslist = _service.GetAllTrainers();
            return View(Trainerslist);
        }
    }
}