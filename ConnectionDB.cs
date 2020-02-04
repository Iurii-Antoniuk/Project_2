using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Project_2
{
    class ConnectionDB
    {
        public static SqlConnection ConfigConnection()
        {
            // Récupère la connectionString dans le fichier app.config
            var connectionString = ConfigurationManager.ConnectionStrings["Projet2_BancAppli"].ConnectionString;
            
            // Requête au langage SQL
            string queryString = "SELECT id, name FROM Person;";

            // Dans le même bloc, créé une connexion qui n'existe que dans ce bloc avec using
            using (var connection = new SqlConnection(connectionString))
            {
                // Créé une commande sur la connection qui executera la queryString
                var command = new SqlCommand(queryString, connection);

                connection.Open();

                // Execute le code si la connexion est ouverte
                if (connection.State == System.Data.ConnectionState.Open )
                {
                    Console.WriteLine("La connexion est ouverte");
                }

               // Lit le flu dans la BDD 
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
                    }
                }
                return connection;
            }

        }

    }
}
