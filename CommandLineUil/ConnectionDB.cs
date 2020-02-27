using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace IKEACmdUtil
{
    class ConnectionDB
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
                    Console.Write("\t" + dataread[item].ToString() +",");
                }
                Console.WriteLine();
            }
            dataread.Close();
            connection.Close();
            Console.WriteLine("DONE");         
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
                
        public static List<int> GetSavingAccountIds(int client_id)
        {
            List<int> ids = new List<int>();
            string queryStringId = $"SELECT id FROM SavingAccounts WHERE client_id = {client_id};"; 
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            SqlCommand command = new SqlCommand(queryStringId, connection);
            SqlDataReader dataread = command.ExecuteReader();
            while (dataread.Read())
            {
                ids.Add((Int32)dataread[0]);
            }
            dataread.Close();
            connection.Close();    
            
            return ids;
        }
    }
}

