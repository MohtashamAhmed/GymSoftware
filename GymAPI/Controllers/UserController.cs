﻿using CommonUtility;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GymAPI.Controllers
{
    public class UserController : ApiController
    {
        UserService _service = new UserService();
        [HttpPost]
        public string UserRegistration(UserModel Registration)
        {
            var result = _service.UserRegistration(Registration);
            return result;
        }
    }
}
