using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Project_2
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
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();
                connection.Close();
                Console.WriteLine("Command executed");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }
        }

        public static void SelectSQL(string queryString, List<string> columnsName)
        {
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataread = command.ExecuteReader();
                while (dataread.Read())
                {
                    foreach (string item in columnsName)
                    {
                        Console.Write(dataread[item].ToString() + "  ");
                    }
                    Console.WriteLine();
                }
                dataread.Close();
                connection.Close();
                Console.WriteLine("DONE");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
            }
            
        }

        public static int ReturnID(string queryString)
        {
            int id = 0;
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataread = command.ExecuteReader();
                dataread.Read();
                id = dataread.GetInt32(0);
                dataread.Close();
                connection.Close();
                Console.WriteLine("DONE");
                return id;
            }
            catch (Exception e)
            {
                Console.WriteLine("Not valid name or password" + e.Message);
                return id;
            }           
        }

        public static string ReturnPassword(int client_id)
        {
            try
            {
                string queryString = $"SELECT password FROM Person WHERE id = '{client_id}'";
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataread = command.ExecuteReader();
                dataread.Read();
                string password = dataread.GetString(2);
                dataread.Close();
                connection.Close();
                Console.WriteLine("DONE");
                return password;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
                return null;
            }
        }

    

        public static int ReturnIdCurrentAccount(int client_id)
        {
            try
            {
                string queryString = $"SELECT id FROM CurrentAccounts WHERE client_id = '{client_id}'";
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataread = command.ExecuteReader();
                dataread.Read();
                int idCurrentAccount = dataread.GetInt32(0);
                dataread.Close();
                connection.Close();
                Console.WriteLine("DONE");
                return idCurrentAccount ;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
                return 0 ;
            }
        }

        public static int ReturnIdSavingAccount(int client_id)
        {
            try
            {
                string queryString = $"SELECT id FROM SavingAccounts WHERE client_id = '{client_id}'";
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataread = command.ExecuteReader();
                dataread.Read();
                int idCurrentAccount = dataread.GetInt32(0);
                dataread.Close();
                connection.Close();
                Console.WriteLine("DONE");
                return idCurrentAccount;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.Message);
                return 0;
            }
        }




    }
}
