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

        public static void NonQuerySQL(string queryString, IEnumerable<SqlParameter> parameters = null)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection); ;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters.ToArray());
            }
            command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Command executed");
        }

        public static decimal ReturnDecimal(string queryString)
        {
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader dataread = command.ExecuteReader();
            dataread.Read();
            decimal amount = dataread.GetDecimal(0);
            dataread.Close();
            connection.Close();
            return amount;
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

        public static List<Transaction> GetPendingTransactionsInTransaction()
        {
            List<object[]> list = GetPendingTransactionsFromDB();
            List<Transaction> transactionList = new List<Transaction>();

            foreach (object[] item in list)
            {
                Transaction transaction = new Transaction();
                if(!(item[0] is DBNull))
                {
                    transaction.emitterCurrentAccountId = Convert.ToInt32(item[0]);
                }
                if (!(item[1] is DBNull))
                {
                    transaction.emitterSavingAccountId = Convert.ToInt32(item[1]);
                    
                }
                if (!(item[2] is DBNull))
                {
                    transaction.beneficiaryCurrentAccountId = Convert.ToInt32(item[2]);
                    
                }
                if (!(item[3] is DBNull))
                {
                    transaction.beneficiarySavingAccountId = Convert.ToInt32(item[3]);
                    
                }
                if (!(item[4] is DBNull))
                {
                    transaction.StartDate = Convert.ToDateTime(item[4]);
                    
                }
                if (!(item[5] is DBNull))
                {
                    transaction.EndDate = Convert.ToDateTime(item[5]);
                    
                }
                if (!(item[6] is DBNull))
                {
                    transaction.intervalDate = Convert.ToInt32(item[6]);
                    
                }
                if (!(item[7] is DBNull))
                {
                    transaction.id = Convert.ToInt32(item[7]);
                    
                }
                if (!(item[8] is DBNull))
                {
                    transaction.amount = Convert.ToDecimal(item[8]);
                }
                transactionList.Add(transaction);
            }

            return transactionList;
        }

        public static List<object[]> GetPendingTransactionsFromDB()
        {
            string queryString =$"SELECT currentAccount_id, savingAccount_id, beneficiaryCurrentAccount_id, " +
                                    $"beneficiarySavingAccount_id, executionDate, lastExecutionDate, intervalDays, id, amount " +
                                    $" FROM \"Transaction\" " +
                                    $"WHERE \"status\" = 'pending'; ";

            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader dataread = command.ExecuteReader();
            List<object[]> data = new List<object[]>();

            while (dataread.Read())
            {
                object[] output = new object[dataread.FieldCount];
                dataread.GetValues(output);
                data.Add(output);
            }
            dataread.Close();
            return data;
        }
        public static int ReturnID(string queryString)
        {
            int id = 0;
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader dataread = command.ExecuteReader();
            dataread.Read();
            id = dataread.GetInt32(0);
            dataread.Close();
            connection.Close();
            return id;
        }
    }
}
