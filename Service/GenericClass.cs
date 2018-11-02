using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class GenericClass
    {
        public SqlConnection GetConnection(string connectionName)
        {
            //string cnstr = ConfigurationSettings.AppSettings[connectionName];
            SqlConnection cn = new SqlConnection("Data Source=MOHTASHAM-PC;Initial Catalog=GymSoftware;Integrated Security=True");
            cn.Open();
            return cn;
        }

        public int ExecuteCommand(string Name, Dictionary<string, SqlParameter> procParameters)
        {
            var sqlCon = GetConnection("");
            SqlCommand cmd = new SqlCommand(Name, sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            if (procParameters != null && procParameters.Count > 0)
            {
                foreach (var procParameter in procParameters)
                {
                    cmd.Parameters.AddWithValue(procParameter.Key, procParameter.Value.SqlValue);
                }
            }
            object obj = cmd.ExecuteScalar();
            return Convert.ToInt32(obj);
        }

        public DataTable ExecuteQuery(string Name, Dictionary<string, SqlParameter> procParameters)
        {
            DataTable dt = new DataTable();
            var sqlCon = GetConnection("");
            SqlCommand cmd = new SqlCommand(Name, sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            if (procParameters != null && procParameters.Count > 0)
            {
                foreach (var procParameter in procParameters)
                {
                    cmd.Parameters.AddWithValue(procParameter.Key,procParameter.Value.SqlValue);
                }
            }
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            Adapter.Fill(dt);
            return dt;
        }
    }
}
