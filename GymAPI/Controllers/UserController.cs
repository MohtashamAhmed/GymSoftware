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
    public class UserController : ApiController
    {
        UserService _service = new UserService();
        [HttpPost]
        public string UserRegistration(CustomerRegistration Registration)
        {
            var result = _service.CustomerRegistration(Registration);
            return result;
        }

        [HttpGet]
        public List<CustomerRegistration> GetAllUser(string FullName , string MobileNo)
        {
            var result = _service.GetAllUser( FullName, MobileNo);
            return result;
        }

        [HttpGet]
        public DashboardModel DashboardDetails()
        {
            return _service.DashboardDetails();
        }

        public List<MembershipDetails> GetMembershipDetails ()
        {
            var result = _service.GetMembershipDetails();
            return result;
        }
        public List <BatchDetails> GetBatchDetails()
        {
            var result = _service.GetBatchDetails();
            return result;
        }
    }

}
