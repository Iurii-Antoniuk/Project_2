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
                Console.WriteLine("Not valid name or password");
                return id;
            }           
        }
        public static decimal ReturnDecimal(string queryString)
        {
            decimal decim = 0;
            try
            {
               SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataread = command.ExecuteReader();
                dataread.Read();
<<<<<<< HEAD
              amount = dataread.GetDecimal(0);
                dataread.Close();
                connection.Close();
                Console.WriteLine("DONE");
                return amount;
            }
            catch (Exception e)
            {
                Console.WriteLine("Not valid account number, bitch!" + e.Message);
                return amount;
            }
        }

        public static decimal ReturnOverdraft(string queryString)
        {
            decimal overdraft = 10000;
            try
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                connection.Open();
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataread = command.ExecuteReader();
                dataread.Read();
                overdraft = dataread.GetDecimal(0);
=======
                decim = dataread.GetDecimal(0);
>>>>>>> 7c9fd1a891e3b97c892c49a7acf478d03a1f75d0
                dataread.Close();
                connection.Close();
                Console.WriteLine("DONE");
                return decim;
            }
            catch (Exception e)
            {
                Console.WriteLine("Not valid account number, bitch!" + e.Message);
                return decim;
            }
<<<<<<< HEAD
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
                
=======
        }          
>>>>>>> 7c9fd1a891e3b97c892c49a7acf478d03a1f75d0
    }

}

