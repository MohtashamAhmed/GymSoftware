using CommonUtility;
using MySql.Data.MySqlClient;
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
        //private static GenericClass _GenClass = new GenericClass();
        private static NewGenericClass _GenClassnew = new NewGenericClass();

        public void test()
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            //Parameter["name"] = new SqlParameter("name", "Smith");
            _GenClassnew.ExecuteCommand("", Parameter);
        }

        #region Customer Registration
        public string CustomerRegistration(CustomerRegistration Registration)
        {
            if (Registration.CustomerID == null && Registration.CustomerID == 0)
            {
                Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
                Parameter["Nam"] = new MySqlParameter("Nam", Registration.Name);
                Parameter["Gen"] = new MySqlParameter("Gen", Registration.Gender);
                Parameter["BirthD"] = new MySqlParameter("BirthD", Registration.DateofBirth);
                Parameter["Mob"] = new MySqlParameter("Mob", Registration.Mobile);
                Parameter["Ema"] = new MySqlParameter("Ema", Registration.Email);
                Parameter["Weigh"] = new MySqlParameter("Weigh", Registration.Weight);
                Parameter["scr"] = new MySqlParameter("scr", Registration.Source);
                Parameter["JoinD"] = new MySqlParameter("JoinD", DateTime.Now);
                Registration.CustomerID = _GenClassnew.ExecuteCommand("SP_AddCustomer", Parameter, true);
            }
            if (Registration.CustomerID > 0)
            {
                Dictionary<string, MySqlParameter> Details = new Dictionary<string, MySqlParameter>();
                int Tid = Registration.TrainerID != null ? Registration.TrainerID : 0;
                Details["CustID"] = new MySqlParameter("CustID", Registration.CustomerID);
                Details["MemID"] = new MySqlParameter("MemID", Registration.MembershipID);
                Details["BID"] = new MySqlParameter("BID", Registration.BatchID);
                Details["TotalP"] = new MySqlParameter("TotalP", Registration.TotalPayment);
                Details["Pay"] = new MySqlParameter("Pay", Registration.Payment);
                Details["Outs"] = new MySqlParameter("Outs", Registration.Outstanding);
                Details["Dop"] = new MySqlParameter("Dop", DateTime.Now);
                Details["ExDate"] = new MySqlParameter("ExDate", GetMembershipExpDate(DateTime.Now, Registration.MembershipID));
                Details["TID"] = new MySqlParameter("TID", Tid);
                //Details["ExistingUser"] = new MySqlParameter("ExistingUser", false);
                int TrainerID = _GenClassnew.ExecuteCommand("SP_AddPayment", Details, true);

                if (Registration.renew)
                    return Registration.Name + " Account renewed successfully!!";

                if (TrainerID > 0)
                    return Registration.Name + " Registered Successfully!!";
            }

            Dictionary<string, MySqlParameter> DeleteParam = new Dictionary<string, MySqlParameter>();
            DeleteParam["custid"] = new MySqlParameter("custid", Registration.CustomerID);
            _GenClassnew.ExecuteCommand("SP_DeleteUser", DeleteParam);
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
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["Nam"] = new MySqlParameter("Nam", Name);
            Parameter["Mob"] = new MySqlParameter("Mob", MobileNo);
            DataTable dt = _GenClassnew.ExecuteQuery("SP_CustomerDetails", Parameter);
            List<DisplayCustomers> UserList = new List<DisplayCustomers>();
            foreach (DataRow row in dt.Rows)
            {
                DisplayCustomers UM = new DisplayCustomers();
                UM.CustomerID = row.Field<int>("CustomerID");
                UM.Name = row.Field<string>("Name");
                UM.Mobile = row.Field<string>("Mobile");
                UM.Email = row.Field<string>("Email");
                UM.MembershipName = row.Field<string>("Memmership");
                UM.BatchName = row.Field<string>("BatchName");
                UM.Payment = row.Field<int>("Payment");
                UM.Outstanding = row.Field<int>("Outstanding");
                UM.TotalPayment = (UM.Payment + UM.Outstanding).ToString();
                UM.DateOfPayment = row.Field<DateTime>("DateOfPayment");
                UM.ExpiryDate = row.Field<DateTime>("ExpiryDate");
                UserList.Add(UM);
            }
            return UserList;
        }
        #endregion

        #region Dashboard
        public DashboardModel DashboardBoxCount()
        {
            DashboardModel Dashboard = new DashboardModel();
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            var res = _GenClassnew.ExecuteQuery("SP_DashboardCount", Parameter);
            DataRow row = res.Rows[0];
            Dashboard.TotalMembers = row["TotalMembers"].ToString();
            Dashboard.FestiveOffer = row["Offers"].ToString();
            Dashboard.MonthlySales = row["MonthlySale"].ToString();
            Dashboard.MonthlySales =(Dashboard.MonthlySales == null || Dashboard.MonthlySales == "") ? "0" : Dashboard.MonthlySales;
            Dashboard.UpcomingBirthdays = row["Birthdays"].ToString();
            return Dashboard;
        }

        public List<Graph> DashboardGraph()
        {
            List<Graph> List = new List<Graph>();
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            var Res = _GenClassnew.ExecuteQuery("SP_Graph", Parameter);
            foreach (DataRow Row in Res.Rows)
            {
                Graph GH = new Graph();
                int id = Row.Field<int>("Month");
                GH.Month = Enum.GetName(typeof(EnumMonth), id);
                GH.Revenue = Convert.ToInt32(Row.Field<decimal>("Revenue"));
                List.Add(GH);
            }
            return List;
        }
        #endregion

        #region Membership Details
        public List<MembershipDetails> GetMembershipDetails()
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            DataTable dt = _GenClassnew.ExecuteQuery("SP_GetMembership", Parameter);
            List<MembershipDetails> MemberList = new List<MembershipDetails>();
            foreach (DataRow row in dt.Rows)
            {
                MembershipDetails MD = new MembershipDetails();
                MD.ID = row.Field<int>("ID");
                MD.Name = row.Field<string>("Name");
                MD.Price = row.Field<int>("Price");
                MD.IsActive = Convert.ToBoolean(row.Field<SByte>("IsActive"));
                MD.isoffer = Convert.ToBoolean(row.Field<SByte>("Offer"));
                MD.Date = row.Field<DateTime>("Date");
                MemberList.Add(MD);
            }

            return MemberList;
        }
        #endregion

        #region BatchDetails
        public List<BatchDetails> GetBatchDetails()
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            DataTable dt = _GenClassnew.ExecuteQuery("SP_GetBatchDetails", Parameter);
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
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["Mobile"] = new MySqlParameter("Mobile", Mobile);
            var res = _GenClassnew.ExecuteQuery("", Parameter);
            DataRow row = res.Rows[0];
            if (row == null)
                return true;

            else
                return false;

        }

        public List<GraphicalReports> GetGraphDetails()
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            DataTable dt = _GenClassnew.ExecuteQuery("", Parameter);
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
        public Admin Login(UserModel UM)
        {
            Admin User = new Admin();
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["un"] = new MySqlParameter("un", UM.UserName);
            Parameter["pw"] = new MySqlParameter("pw", UM.Password);
            var res = _GenClassnew.ExecuteQuery("SP_AuthenticateUser", Parameter);
            if (res == null)
                return User;

            DataRow row = res.Rows[0];
            User.AdminID = row.Field<int>("ID");
            User.Name = row.Field<string>("Name");
            User.Mobile = row.Field<string>("Mobile");
            User.Email = row.Field<string>("Email");
            return User;
        }
        public List<CustomerRegistration> Receipts()
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["Nam"] = new MySqlParameter("Nam", "");
            Parameter["Mob"] = new MySqlParameter("Mob", "");
            DataTable dt = _GenClassnew.ExecuteQuery("SP_CustomerDetails", Parameter);
            List<CustomerRegistration> UserList = new List<CustomerRegistration>();
            foreach (DataRow row in dt.Rows)
            {
                CustomerRegistration UM = new CustomerRegistration();
                UM.CustomerID = row.Field<int>("CustomerID");
                UM.Name = row.Field<string>("Name");
                UM.Gender = row.Field<string>("Gender");
                UM.Mobile = row.Field<string>("Mobile");
                UM.Email = row.Field<string>("Email");
                UM.Payment = row.Field<int>("Payment");
                UM.DateOfPayment = row.Field<DateTime>("DateOfPayment");
                UM.ExpiryDate = row.Field<DateTime>("ExpiryDate");
                UM.Source = row.Field<string>("Source");
                UserList.Add(UM);
            }
            return UserList;
        }
        public EmailModel GetEmailList()
        {
            EmailModel Model = new EmailModel();
            Model.ReminderList = new List<ReminderEmail>();
            Model.BdayList = new List<BdayEmail>();

            //Get Reminder List
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            DataTable dt = _GenClassnew.ExecuteQuery("SP_ReminderList", Parameter);
            foreach (DataRow row in dt.Rows)
            {
                ReminderEmail reminder = new ReminderEmail();
                reminder.Name = row.Field<string>("Name");
                reminder.ExpiryDate = row.Field<DateTime>("ExpiryDate");
                reminder.days = Convert.ToInt32(row.Field<Int64>("days"));
                Model.ReminderList.Add(reminder);
            }

            //Get Birthday List
            DataTable dt1 = _GenClassnew.ExecuteQuery("SP_Bdayreminder", Parameter);
            foreach (DataRow row in dt1.Rows)
            {
                BdayEmail breminder = new BdayEmail();
                breminder.Name = row.Field<string>("Name");
                breminder.Mobile = row.Field<string>("Mobile");
                breminder.Email = row.Field<string>("Email");
                breminder.Age = Convert.ToInt32(row.Field<Int64>("Age"));
                Model.BdayList.Add(breminder);
            }
            return Model;
        }

        public string AddOffer(MembershipDetails Offer)
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["nam"] = new MySqlParameter("nam", Offer.Name);
            Parameter["pri"] = new MySqlParameter("pri", Offer.Price);
            Parameter["isact"] = new MySqlParameter("isact", true);
            Parameter["off"] = new MySqlParameter("off", Offer.isoffer);
            Parameter["dat"] = new MySqlParameter("dat", DateTime.Now);
            int ID = _GenClassnew.ExecuteCommand("SP_Addfestiveoffer", Parameter, true);
            return (ID > 0 && ID != null) ? "Added Succesfully!!" : "something went wrong";
        }

        public string UpdateOffer(int ID, bool active)
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["memid"] = new MySqlParameter("memid", ID);
            Parameter["isact"] = new MySqlParameter("isact", active);
            _GenClassnew.ExecuteCommand("SP_UpdateOffer", Parameter);
            return "Updated Succesfully!!";
        }

        public List<DisplayCustomers> GetCustomerHistory(int custID)
        {
            List<DisplayCustomers> CustList = new List<DisplayCustomers>();            
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["CustID"] = new MySqlParameter("CustID", custID);
            DataTable dt = _GenClassnew.ExecuteQuery("SP_CustomerHistory", Parameter);
            foreach (DataRow row in dt.Rows)
            {
                DisplayCustomers history = new DisplayCustomers();
                history.Name = row.Field<string>("Name");
                history.MembershipName = row.Field<string>("Memmership");
                history.BatchName = row.Field<string>("BatchName");
                history.date = row.Field<DateTime>("DateOfPayment").ToString("dd/MM/yyyy");
                CustList.Add(history);
            }
            return CustList;
        }

    }


}
