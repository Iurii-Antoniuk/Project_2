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
            // Récupère la connectionString dans le fichier app.config
            string connectionString = ConfigurationManager.ConnectionStrings["Projet2_BancAppli"].ConnectionString;
            
                // Execute le code si la connexion est ouverte
                /*if (connection.State == System.Data.ConnectionState.Open )
                {
                    Console.WriteLine("La connexion est ouverte");
                }*/

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
                Console.WriteLine("Fuck you dumbass, dickhead, cunt! And the client too... " + e.Message);
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
                Console.WriteLine("Fuck you dumbass, dickhead, cunt! And the client too... " + e.Message);
            }
            
        }

    }
}
