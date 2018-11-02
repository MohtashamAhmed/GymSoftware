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
    public class TrainerService
    {
        private static GenericClass _GenClass = new GenericClass();

        public string TrainerRegistration(TrainerModel Trainer)
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            Parameter["TrainerName"] = new SqlParameter("TrainerName", Trainer.TrainerName);
            Parameter["Gender"] = new SqlParameter("Gender", Trainer.Gender);
            Parameter["Mobile"] = new SqlParameter("Mobile", Trainer.Mobile);
            Parameter["Email"] = new SqlParameter("Email", Trainer.Email);
            Parameter["JoinDate"] = new SqlParameter("JoinDate", DateTime.Now);
            int id = _GenClass.ExecuteCommand("SP_AddTrainer", Parameter);
            if (id > 0)
                return Trainer.TrainerName + " Registered Successfully!!";

            else
                return "Registration Failed";
        }

        public List<TrainerModel> GetAllTrainers()
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            DataTable dt = _GenClass.ExecuteQuery("SP_GetAllTrainers", Parameter);
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



    }
}
