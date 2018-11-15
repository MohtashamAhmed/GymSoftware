using CommonUtility;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymSoftware.Controllers
{
    [Authorize]
    public class TrainerController : BaseController
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
            ViewBag.Tmessage = _service.TrainerRegistration(Trainer);
            ModelState.Clear();
            return View();
        }

        public ActionResult GetAllTrainer()
        {
            var Trainerslist = _service.GetAllTrainers();
            return View(Trainerslist);
        }
    }
}