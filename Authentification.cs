using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Authentification
    {
        public static void Login(string name, string password)
        {

            string queryString = $"SELECT id FROM Person (name, password) VALUES ('{name}', '{password}');";
            int id = ConnectionDB.ReturnID(queryString);

            if (id == 1)
            {

            }


            /*if (Login && mot de passe == Admin)
            {
                new admin
            }
            else if (Login && mot de passe != admin)
            {
                new client
            }
            else (Login et mot de passe != BDD)
            {
                fuck you
            }

            try
            {
                    SELECT name, password FROM Person WHERE name = name && password = password;

                if name = admin
                    name = client
            }
            catch (Exception e)
            {
                "No login"
            }*/
        }
    }
}