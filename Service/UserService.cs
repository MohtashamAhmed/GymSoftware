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
        private static GenericClass _GenClass = new GenericClass();

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
            Parameter["Gender"] = new SqlParameter("Gender", Registration.Gender);
            Parameter["DOB"] = new SqlParameter("DOB", Registration.DateofBirth);
            Parameter["Mobile"] = new SqlParameter("Mobile", Registration.Mobile);
            Parameter["Email"] = new SqlParameter("Email", Registration.Email);
            Parameter["Weight"] = new SqlParameter("Weight", Registration.Weight);
            Parameter["Source"] = new SqlParameter("Source", Registration.Source);
            Parameter["JoinDate"] = new SqlParameter("JoinDate", DateTime.Now);
            int CustomerID = _GenClass.ExecuteCommand("SP_AddCustomer", Parameter);
            if (CustomerID > 0)
            {
                Dictionary<string, SqlParameter> Details = new Dictionary<string, SqlParameter>();
                int Tid = Registration.TrainerID != null ? Registration.TrainerID : 0;
                Details["CustomerID"] = new SqlParameter("CustomerID", CustomerID);
                Details["MembershipID"] = new SqlParameter("MembershipID", Registration.MembershipID);
                Details["BatchID"] = new SqlParameter("BatchID", Registration.BatchID);
                Details["TotalPayment"] = new SqlParameter("TotalPayment", Registration.TotalPayment);
                Details["Payment"] = new SqlParameter("Payment", Registration.Payment);
                Details["Outstanding"] = new SqlParameter("Outstanding", Registration.Outstanding);
                Details["DateOfPayment"] = new SqlParameter("DateOfPayment", DateTime.Now);
                Details["ExpiryDate"] = new SqlParameter("ExpiryDate", GetMembershipExpDate(DateTime.Now, Registration.MembershipID));
                Details["TrainerID"] = new SqlParameter("TrainerID", Tid);
                Details["ExistingUser"] = new SqlParameter("ExistingUser", false);
                int TrainerID = _GenClass.ExecuteCommand("SP_AddPayment", Details);
                if (TrainerID > 0)
                    return Registration.Name + " Registered Successfully";
            }
            Dictionary<string, SqlParameter> DeleteParam = new Dictionary<string, SqlParameter>();
            DeleteParam["id"] = new SqlParameter("id", CustomerID);
            _GenClass.ExecuteCommand("SP_DeleteUser", DeleteParam);
            return "Registration Failed";
        }

        public DateTime GetMembershipExpDate(DateTime Date, int MembershipID)
        {
            DateTime ExpDate = Date;
            switch (MembershipID)
            {
                case 1:
                    ExpDate = Date.AddMonths(1);
                    break;

                case 2:
                    ExpDate = Date.AddMonths(3);
                    break;

                case 3:
                    ExpDate = Date.AddMonths(6);
                    break;

                case 4:
                    ExpDate = Date.AddYears(1);
                    break;

                default:
                    break;
            }
            return ExpDate;
        }

        #endregion

        #region Retrieving All Users
        public List<DisplayCustomers> GetAllUser(string Name, string MobileNo)
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            Parameter["Name"] = new SqlParameter("Name", Name);
            Parameter["Mobile"] = new SqlParameter("Mobile", MobileNo);
            DataTable dt = _GenClass.ExecuteQuery("SP_CustomerDetails", Parameter);
            List<DisplayCustomers> UserList = new List<DisplayCustomers>();
            foreach (DataRow row in dt.Rows)
            {
                DisplayCustomers UM = new DisplayCustomers();
                UM.Name = row.Field<string>("Name");
                UM.Mobile = row.Field<string>("Mobile");
                UM.Email = row.Field<string>("Email");
                UM.MembershipName = row.Field<string>("Memmership");
                UM.BatchName = row.Field<string>("BatchName");
                UM.Payment = row.Field<int>("Payment");
                UM.Outstanding = row.Field<int>("Outstanding");
                UM.DateOfPayment = row.Field<DateTime>("DateOfPayment");
                UM.ExpiryDate = row.Field<DateTime>("ExpiryDate");
                UserList.Add(UM);
            }
            return UserList;
        }
        #endregion

        #region Dashboard
        public DashboardModel DashboardDetails()
        {
            DashboardModel Dashboard = new DashboardModel();
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            var res = _GenClass.ExecuteQuery("", Parameter);
            DataRow row = res.Rows[0];
            Dashboard.UpcomingBirthdays = row["UpcomingBirthdays"].ToString();
            Dashboard.Notifications = row["Notifications"].ToString();
            return Dashboard;
        }
        #endregion

        #region Membership Details
        public List<MembershipDetails> GetMembershipDetails()
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            DataTable dt = _GenClass.ExecuteQuery("SP_GetMembership", Parameter);
            List<MembershipDetails> MemberList = new List<MembershipDetails>();
            foreach (DataRow row in dt.Rows)
            {
                MembershipDetails MD = new MembershipDetails();
                MD.ID = row.Field<int>("ID");
                MD.Name = row.Field<string>("Name");
                MD.Price = row.Field<int>("Price");
                MemberList.Add(MD);
            }

            return MemberList;
        }
        #endregion

        #region BatchDetails
        public List<BatchDetails> GetBatchDetails()
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            DataTable dt = _GenClass.ExecuteQuery("SP_GetBatchDetails", Parameter);
            List<BatchDetails> BatchDetails = new List<BatchDetails>();
            foreach (DataRow row in dt.Rows)
            {
                BatchDetails BD = new BatchDetails();
                BD.ID = row.Field<int>("ID");
                BD.BatchName = row.Field<string>("BatchName");
                BD.BatchName = row.Field<string>("BatchName") + " (" + row.Field<string>("Timings") + ")";
                BD.Timings = row.Field<string>("Timings");
                BatchDetails.Add(BD);
            }
            return BatchDetails;
        }
        #endregion
        public bool IsUserAvailable(string Mobile)
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            Parameter["Mobile"] = new SqlParameter("Mobile", Mobile);
            var res = _GenClass.ExecuteQuery("", Parameter);
            DataRow row = res.Rows[0];
            if (row == null)
                return true;

            else
                return false;

        }

        public List<GraphicalReports> GetGraphDetails()
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            DataTable dt = _GenClass.ExecuteQuery("", Parameter);
            List<GraphicalReports> Graph = new List<GraphicalReports>();
            foreach (DataRow row in dt.Rows)
            {
                GraphicalReports GD = new GraphicalReports();
                GD.Sales = row.Field<string>("Sales");
                GD.StartDate = row.Field<DateTime>("StartDate");
                GD.EndDate = row.Field<DateTime>("EndDate");
                Graph.Add(GD);
            }

            return Graph;
        }
        public string Login( UserModel UM)
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            var res = _GenClass.ExecuteQuery("", Parameter);
            return "";
       }
        public List<CustomerRegistration> Receipts()
        {
            
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            Parameter["Name"] = new SqlParameter("Name", "");
            Parameter["Mobile"] = new SqlParameter("Mobile", "");
            DataTable dt = _GenClass.ExecuteQuery("SP_CustomerDetails", Parameter);
            List<Receipts> Receipts = new List<Receipts>();
            List<CustomerRegistration> UserList = new List<CustomerRegistration>();
            //foreach (DataRow row in dt.Rows)
            //{
            //   Receipts TR = new Receipts();
            //    TR.CustomerName = row.Field<string>("CustomerName");
            //    TR.TotalReceipts = row.Field<string>("TotalReceipts");
            //    TR.Date = row.Field<DateTime>("Date");
            //    TR.TotalAmount = row.Field<string>("TotalAount");
            //    Receipts.Add(TR);
            //}
            foreach (DataRow row in dt.Rows)
            {
                CustomerRegistration UM = new CustomerRegistration();
                UM.Name = row.Field<string>("Name");
                UM.Payment = row.Field<int>("Payment");
                UM.DateOfPayment = row.Field<DateTime>("DateOfPayment");
                UM.ExpiryDate = row.Field<DateTime>("ExpiryDate");
                UserList.Add(UM);
            }
            return UserList;
        }

    }


}
