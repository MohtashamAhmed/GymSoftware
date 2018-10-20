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
            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-6H0DS1N;Initial Catalog=hrms01;Integrated Security=True");
            cn.Open();
            return cn;
        }

        public void ExecuteCommand(string Name, Dictionary<string, SqlParameter> procParameters)
        {
            var sqlCon = GetConnection("");
            SqlCommand cmd = new SqlCommand(Name, sqlCon);
            cmd.CommandType = CommandType.StoredProcedure;
            if (procParameters != null && procParameters.Count > 0)
            {
                foreach (var procParameter in procParameters)
                {
                    cmd.Parameters.Add(procParameter.Value);
                }
            }
            int rc = cmd.ExecuteNonQuery();
        }

        public DataSet ExecuteQuery(string Name, Dictionary<string, SqlParameter> procParameters)
        {
            DataSet ds = new DataSet();
            var sqlCon = GetConnection("");
            SqlCommand cmd = new SqlCommand(Name, sqlCon);
            if (procParameters != null && procParameters.Count > 0)
            {
                foreach (var procParameter in procParameters)
                {
                    cmd.Parameters.Add(procParameter.Value);
                }
            }
            SqlDataAdapter Adapter = new SqlDataAdapter(cmd);
            Adapter.Fill(ds);
            return ds;
        }
    }
}
