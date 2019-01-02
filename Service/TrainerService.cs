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
    public class TrainerService
    {
        //private static GenericClass _GenClass = new GenericClass();
        private static NewGenericClass _GenClassnew = new NewGenericClass();

        public string TrainerRegistration(TrainerModel Trainer)
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["Tn"] = new MySqlParameter("Tn", Trainer.TrainerName);
            Parameter["Gen"] = new MySqlParameter("Gen", Trainer.Gender);
            Parameter["Mob"] = new MySqlParameter("Mob", Trainer.Mobile);
            Parameter["Ema"] = new MySqlParameter("Ema", Trainer.Email);
            int id = _GenClassnew.ExecuteCommand("SP_AddTrainer", Parameter,true);
            if (id > 0)
                return Trainer.TrainerName + " Registered Successfully!!";

            else
                return "Registration Failed";
        }

        public List<TrainerModel> GetAllTrainers()
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            DataTable dt = _GenClassnew.ExecuteQuery("SP_GetAllTrainers", Parameter);
            List<TrainerModel> Trainers = new List<TrainerModel>();
            foreach (DataRow row in dt.Rows)
            {
                TrainerModel trainer = new TrainerModel();
                trainer.ID = row.Field<int>("ID");
                trainer.TrainerName = row.Field<string>("TrainerName");
                trainer.Gender = row.Field<string>("Gender");
                trainer.Mobile = row.Field<string>("Mobile");
                trainer.Email = row.Field<string>("Email");
                trainer.JoinDate = row.Field<DateTime>("JoinDate");
                Trainers.Add(trainer);
            }
            return Trainers;
        }

        public bool CheckMobile(string mobile, string name)
        {
            Dictionary<string, MySqlParameter> Parameter = new Dictionary<string, MySqlParameter>();
            Parameter["mob"] = new MySqlParameter("mob", mobile);
            Parameter["nam"] = new MySqlParameter("nam", name);
            DataTable dt = _GenClassnew.ExecuteQuery("SP_CheckMobile", Parameter);
            return dt != null && dt.Rows.Count > 0 ? false : true;
        }

    }
}
