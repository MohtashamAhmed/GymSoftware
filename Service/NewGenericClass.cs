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

        public int ExecuteCommand(string Name, Dictionary<string, MySqlParameter> procParameters)
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
            object obj = cmd.ExecuteScalar();
            return Convert.ToInt32(obj);
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
