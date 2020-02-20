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
                return id;
            }
            catch (Exception)
            {
                Console.WriteLine("Not valid name or password" );
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
                decimal amount = dataread.GetDecimal(0);
                dataread.Close();
                connection.Close();
                return amount;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
                return decim;
            }
        }

        /*public static decimal ReturnOverdraft(string queryString)
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

                decimal decim = dataread.GetDecimal(0);

                dataread.Close();
                connection.Close();
                Console.WriteLine("DONE");
                return decim;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.Message);
                return 0;
            }
        }*/
                            
        public static List<decimal> GetAccountColumnValues(string tableName, string columnName)
        {
            List<decimal> values = new List<decimal>();
            int numberOFAccounts = SavingsAccount.CountRows(tableName);
            SqlConnection connection = new SqlConnection(GetConnectionString());
            connection.Open();
            for (int i = 0; i < numberOFAccounts; i++)
            {
                string queryString = $"SELECT {columnName} FROM {tableName} WHERE id = 100 + {i};";
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader dataread = command.ExecuteReader();
                dataread.Read();
                decimal decim = dataread.GetDecimal(0);
                values.Add(decim);
                dataread.Close();
            }
            connection.Close();
            return values;
        }
    }
}

