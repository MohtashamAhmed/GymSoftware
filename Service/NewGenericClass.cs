using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class NewGenericClass
    {
        public MySqlConnection GetConnection(string connectionName)
        {
            string cnstr = "datasource=148.72.232.174;port=3306;Database=GymManagement;username=LuckyRiya;Password=luri@9293";
            MySqlConnection cn = new MySqlConnection(cnstr);
            cn.Open();
            return cn;
        }

        public int ExecuteCommand(string Name, Dictionary<string, MySqlParameter> procParameters, bool op = false)
        {
            var sqlCon = GetConnection("");
            MySqlCommand cmd = new MySqlCommand(Name, sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            if (procParameters != null && procParameters.Count > 0)
            {
                foreach (var procParameter in procParameters)
                {
                    cmd.Parameters.AddWithValue(procParameter.Key, procParameter.Value.Value);
                }
            }
            int ID = 0;
            if (op)
            {
                cmd.Parameters.Add("LID", SqlDbType.Int);
                cmd.Parameters["LID"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                ID = (int)cmd.Parameters["LID"].Value;
                //object obj = cmd.ExecuteScalar();
            }
            else
            {
                cmd.ExecuteNonQuery();
            }
            return ID;
        }

        public DataTable ExecuteQuery(string Name, Dictionary<string, MySqlParameter> procParameters)
        {
            DataTable dt = new DataTable();
            var sqlCon = GetConnection("");
            MySqlCommand cmd = new MySqlCommand(Name, sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            if (procParameters != null && procParameters.Count > 0)
            {
                foreach (var procParameter in procParameters)
                {
                    cmd.Parameters.AddWithValue(procParameter.Key, procParameter.Value.Value);
                }
            }
            MySqlDataAdapter Adapter = new MySqlDataAdapter(cmd);
            Adapter.Fill(dt);
            return dt;
        }
    }
}
