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
            Parameter["Gender"] = new SqlParameter("TrainerAvailability", Trainer.Gender);
            Parameter["Mobile"] = new SqlParameter("TrainerAvailability", Trainer.Mobile);
            Parameter["Email"] = new SqlParameter("TrainerAvailability", Trainer.Email);
            Parameter["JoinDate"] = new SqlParameter("TrainerAvailability", DateTime.Now);
            DataTable dt = _GenClass.ExecuteQuery("SP_AddTrainer", Parameter);
            if (dt != null && dt.Rows.Count > 0)
            {
                return "New ID generated";
            }
            else
            {
                return "Not added";
            }
        }
        public List<TrainerModel> GetAllTrainers()
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            DataTable dt = _GenClass.ExecuteQuery("SP_AddTrainer", Parameter);
            List<TrainerModel> Trainers = new List<TrainerModel>();
            foreach (DataRow row in dt.Rows)
            {
                TrainerModel trainer = new TrainerModel();
                trainer.ID = row.Field<int>("ID");
                trainer.TrainerName = row.Field<string>("TrainerName");
                trainer.Gender = row.Field<string>("Gender");
                trainer.Mobile = row.Field<string>("Mobile");
                trainer.Email = row.Field<string>("Email");
                trainer.JoinDate = row.Field<string>("JoinDate");
                Trainers.Add(trainer);
            }
            return Trainers;
        }



    }
}
