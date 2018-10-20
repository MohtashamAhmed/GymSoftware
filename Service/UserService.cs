using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService
    {
        private  GenericClass _GenClass;

        public void test()
        {
            Dictionary<string, SqlParameter> Parameter = new Dictionary<string, SqlParameter>();
            //Parameter["name"] = new SqlParameter("name", "Smith");
            _GenClass.ExecuteCommand("", Parameter);
        }

    }
}
