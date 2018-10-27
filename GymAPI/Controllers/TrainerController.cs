using CommonUtility;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymAPI.Controllers
{
    public class TrainerController : ApiController
    {
        TrainerService _Trainerservice = new TrainerService();
        public string TrainerRegistration(TrainerModel Trainer)
        {
            var result = _Trainerservice.TrainerRegistration(Trainer);
            return result;
        }
        [HttpGet]
        public List<TrainerModel> GetAllTrainers()
        {
            var result = _Trainerservice.GetAllTrainers();
            return result;
        }
    }
}
