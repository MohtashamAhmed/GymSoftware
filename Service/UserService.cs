using CommonUtility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        private GenericClass _GenClass;

        public void test()
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            //Parameter["name"] = new SqlParameter("name", "Smith");
            _GenClass.ExecuteCommand("", Parameter);
        }

        public string UserRegistration(UserModel Registration)
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            Parameter["FirstName"] = new SqlParameter("FirstName", Registration.FirstName);
            Parameter["LastName"] = new SqlParameter("LastName", Registration.FirstName);
            Parameter["EmailID"] = new SqlParameter("EmailID", Registration.EmailID);
            Parameter["Gender"] = new SqlParameter("Gender", Registration.Gender);
            Parameter["DateofBirth"] = new SqlParameter("DateofBirth", Registration.DateofBirth);
            Parameter["Membership"] = new SqlParameter("Membership", Registration.Membership);
            Parameter["Address1"] = new SqlParameter("Address1", Registration.Address1);
            Parameter["Address2"] = new SqlParameter("Address2", Registration.Address2);
            Parameter["City"] = new SqlParameter("City", Registration.City);
            Parameter["State"] = new SqlParameter("State", Registration.State);
            Parameter["Postcode"] = new SqlParameter("Postcode", Registration.Postcode);
            int rc = _GenClass.ExecuteCommand("", Parameter);
            if (rc > 0)
            {
                return "New Customer Added";
            }
            else
            {
                return "Not Added";
            }
        }
    }
}
