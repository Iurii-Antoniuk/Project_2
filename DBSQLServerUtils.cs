using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Project_2
{
    class DBSQLServerUtils
    {   // Note: il faut installer le package NuGet *System.Data.SqlClient*
        public static SqlConnection
                    GetDBConnection(string datasource, string database, string username, string password)
        {
            //
            // Data Source=PC-HARU\SQLEXPRESS;Initial Catalog=Projet2_BancAppli;User ID=UserAdmin;Password=***********
            //
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }
    }
}
