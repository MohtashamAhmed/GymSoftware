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

        #region Customer Registration
        public string CustomerRegistration(CustomerRegistration Registration)
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            Parameter["Name"] = new SqlParameter("Name", Registration.Name);
            Parameter["Mobile"] = new SqlParameter("Mobile", Registration.Mobile);
            Parameter["Email"] = new SqlParameter("Email", Registration.Email);
            Parameter["Gender"] = new SqlParameter("Gender", Registration.Gender);
            Parameter["Source"] = new SqlParameter("Source", Registration.Source);
            Parameter["DateofBirth"] = new SqlParameter("DateofBirth", Registration.DateofBirth);
            Parameter["Weight"] = new SqlParameter("Membership", Registration.Weight);
            Parameter["JoinDate"] = new SqlParameter("JoinDate", Registration.JoinDate);
            int CustomerID = _GenClass.ExecuteCommand("", Parameter);
            if (CustomerID > 0)
            {
                Dictionary<string, SqlParameter> Details = new Dictionary<string, SqlParameter>();
                Details["CustomerID"] = new SqlParameter("CustomerID", CustomerID);
                Details["TrainerID"] = new SqlParameter("TrainerID", Registration.TrainerID);
                Details["JoinDate"] = new SqlParameter("JoinDate", Registration.JoinDate);
                Details["MembershipID"] = new SqlParameter("MembershipID", Registration.MembershipID);
                Details["BatchID"] = new SqlParameter("BatchID", Registration.BatchID);
                Details["TotalPayment"] = new SqlParameter("TotalPayment", Registration.TotalPayment);
                Details["Payment"] = new SqlParameter("Payment", Registration.Payment);
                Details["Outstanding"] = new SqlParameter("Outstanding", Registration.Outstanding);
                Details["DateOfPayment"] = new SqlParameter("DateOfPayment", Registration.DateOfPayment);
                Details["ExpiryDate"] = new SqlParameter("ExpiryDate", Registration.ExpiryDate);
                Details["ExistingUser"] = new SqlParameter("ExistingUser", Registration.ExistingUser);
                int TrainerID = _GenClass.ExecuteCommand("", Details);
                if (TrainerID > 0)
                {

                }
                return "Customer Added";
            }
            else
            {
                return "Not Added";
            }
        }
        #endregion

        #region Retrieving All Users
        public List<CustomerRegistration> GetAllUser(string Name, string MobileNo)
        {
            CustomerRegistration UM = new CustomerRegistration();
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            Parameter["Name"] = new SqlParameter("Name", UM.Name);
            Parameter["Mobile"] = new SqlParameter("Mobile", UM.Mobile);
            DataTable dt = _GenClass.ExecuteQuery("", Parameter);
            List<CustomerRegistration> UserList = new List<CustomerRegistration>();
            foreach (DataRow row in dt.Rows)
            {
                UM.Name = row.Field<string>("Name");
                UM.Mobile = row.Field<string>("Mobile");
                UM.Email = row.Field<string>("Email");
                UM.Gender = row.Field<string>("Gender");
                UM.Source = row.Field<string>("Source");
                UM.DateofBirth = row.Field<string>("DateofBirth");
                UM.Weight = row.Field<string>("Weight");
                UM.JoinDate = row.Field<string>("JoinDate");
                UserList.Add(UM);
            }
            return UserList;
        }
        #endregion

        #region Dashboard
        public DashboardModel DashboardDetails()
        {
            DashboardModel dash = new DashboardModel();
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            var res = _GenClass.ExecuteQuery("", Parameter);
            DataRow row = res.Rows[0];
            dash.MonthlySales = row["MonthlySales"].ToString();
            dash.QuaterlySales = row["QuaterlySales"].ToString();
            dash.YearlySales = row["YearlySales"].ToString();
            return dash;
        } 
        #endregion
        #region Membership Details
        public List<MembershipDetails> GetMembershipDetails()
        {
            MembershipDetails MD = new MembershipDetails();
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            DataTable dt = _GenClass.ExecuteQuery("", Parameter);
            List<MembershipDetails> MemberList = new List<MembershipDetails>();
            foreach (DataRow row in dt.Rows)
            {
                MD.ID = row.Field<string>("ID");
                MD.Name = row.Field<string>("Name");
                MD.Price = row.Field<string>("Price");
                MemberList.Add(MD);
            }

            return MemberList;

            #endregion
        }
        #region BatchDetails
        public List<BatchDetails> GetBatchDetails()
        {
            BatchDetails BD = new BatchDetails();
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            DataTable dt = _GenClass.ExecuteQuery("", Parameter);
            List<BatchDetails> BatchDetails = new List<BatchDetails>();
            foreach (DataRow row in dt.Rows)
            {
                BD.ID = row.Field<string>("ID");
                BD.BatchName = row.Field<string>("BatchName");
                BD.Timings = row.Field<string>("Timings");
                BatchDetails.Add(BD);
            }
            return BatchDetails;
        } 
        #endregion

    }

}
