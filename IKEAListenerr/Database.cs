using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEAListenerr
{
    class Database
    {
        public static string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Projet2_BancAppli"].ConnectionString;
            return connectionString;
        }
        public static void NonQuerySQL(string queryString)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Command executed");
        }

        public IEnumerable<Transaction> GetPendingTransactions(DateTime startDate, DateTime endDate)
        {

        }

        public void UpdateTransaction(Transaction transaction)
        {

        }
    }
}
