using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

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

        public static void SelectSQL(string queryString, List<string> columnsName)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader dataread = command.ExecuteReader();
            while (dataread.Read())
            {
                foreach (string item in columnsName)
                {
                    Console.Write("\t" + dataread[item].ToString() + ",");
                }
                Console.WriteLine();
            }
            dataread.Close();
            connection.Close();
            Console.WriteLine("DONE");
        }

        public static void TestDBConnection()
        {
            List<string> columnsName = new List<string>();
            columnsName.Add("id");
            columnsName.Add("client_id");
            columnsName.Add("name");
            string query = $"SELECT * FROM Donator;";
            SelectSQL(query, columnsName);
        }

        public IEnumerable<Transaction> GetPendingTransactions(DateTime startDate, DateTime endDate)
        {
            IEnumerable<Transaction> transactionList = new List<Transaction>();
            return transactionList;
        }

        public void UpdateTransaction(Transaction transaction)
        {

        }
    }
}
