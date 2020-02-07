﻿using System;
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
                decim = dataread.GetDecimal(0);
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
        }          
    }

}

