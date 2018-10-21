using CommonUtility;
using System;
using System.Collections.Generic;
using System.Data;
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
            Parameter["LastName"] = new SqlParameter("LastName", Registration.LastName);
            Parameter["FullName"] = new SqlParameter("FullName", Registration.FullName);
            Parameter["EmailID"] = new SqlParameter("EmailID", Registration.EmailID);
            Parameter["Gender"] = new SqlParameter("Gender", Registration.Gender);
            Parameter["DateofBirth"] = new SqlParameter("DateofBirth", Registration.DateofBirth);
            Parameter["Membership"] = new SqlParameter("Membership", Registration.Membership);
            Parameter["Address1"] = new SqlParameter("Address1", Registration.Address1);
            Parameter["Address2"] = new SqlParameter("Address2", Registration.Address2);
            Parameter["City"] = new SqlParameter("City", Registration.City);
            Parameter["State"] = new SqlParameter("State", Registration.State);
            Parameter["Postcode"] = new SqlParameter("Postcode", Registration.Postcode);
            Parameter["MobileNo"] = new SqlParameter("FullName", Registration.MobileNo);
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
        public List<UserModel> GetAllUser (string Name , string MobileNo)
        {
            UserModel UM = new UserModel();
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            Parameter["FullName"] = new SqlParameter("FullName", UM.FullName);
            Parameter["MobileNo"] = new SqlParameter("FullName", UM.MobileNo);
            DataTable dt = _GenClass.ExecuteQuery("",Parameter);
            List<UserModel> UserList = new List<UserModel>();
            foreach(DataRow row in dt.Rows)
            {
                UM.FirstName = row.Field<string>("FirstName");
                UM.LastName = row.Field<string>("LastName");
                UM.FullName = row.Field<string>("Fullname");
                UM.Gender = row.Field<string>("Gender");
                UM.DateofBirth = row.Field<string>("DateofBirth");
                UM.Membership = row.Field<string>("Membership");
                UM.Address1 = row.Field<string>("Address1");
                UM.Address2 = row.Field<string>("Address2");
                UM.City= row.Field<string>("City");
                UM.State = row.Field<string>("State");
                UM.Postcode = row.Field<string>("EmailID");
                UM.MobileNo = row.Field<string>("MobileNo");
                UserList.Add(UM);
            }
            return UserList;
        }
        
    }
    public string Dashboard(DashboardModel DM)
    {
        Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
        Parameter["monthlysales"] = new SqlParameter("monthlysales", DM.MonthlySales);
        Parameter["quaterlysales"] = new SqlParameter("quaterlysales", DM.QuaterlySales);
        Parameter["yearlysales"] = new SqlParameter("yearlysales", DM.YearlySales);
    }
}
