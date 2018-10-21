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
        private GenericClass _GenClass;
        //public string TrainerRegistration(TrainerModel Trainer)
        //{
        //    Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
        //    //Parameter["TrainerID"] = new SqlParameter("TrainerID", Trainer.TrainerID);
        //    //Parameter["TrainerName"] = new SqlParameter("TrainerName", Trainer.TrainerName);
        //    //Parameter["TrainerAvailability"] = new SqlParameter("TrainerAvailability", Trainer.TrainerAvailability);
        //    //DataSet ds = _GenClass.ExecuteQuery("", Parameter);
        //    //if (ds!=  null && ds.Rows.Count >0)
        //    //{
        //    //    return "New ID generated";
        //    //}
        //    //else
        //    //{
        //    //    return "Not added";
        //    //}
        //}
        public List<TrainerModel> GetAllTrainers()
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            DataTable dt = _GenClass.ExecuteQuery("", Parameter);
            List<TrainerModel> Trainers = new List<TrainerModel>();
            foreach (DataRow row in dt.Rows)
            {
                TrainerModel trainer = new TrainerModel();
                trainer.TrainerID = row.Field<int>("TrainerID");
                trainer.TrainerName = row.Field<string>("TrainerName");
                trainer.TrainerAvailability = row.Field<string>("TrainerAvailability");
                Trainers.Add(trainer);
            }
            return Trainers;
        }



    }
}
