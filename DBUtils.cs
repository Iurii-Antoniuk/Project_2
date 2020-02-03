using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Project_2
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            //
            // Data Source=PC-HARU\SQLEXPRESS;Initial Catalog=Projet2_BancAppli;User ID=UserAdmin;Password=***********
            //
            string datasource = @"PC-HARU\SQLEXPRESS";

            string database = "Projet2_BancAppli";
            string username = "UserAdmin";
            string password = "MotDePasse";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}
