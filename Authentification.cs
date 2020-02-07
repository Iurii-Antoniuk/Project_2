using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Security;

namespace Project_2
{
    public class Authentification
    {
        public void Login(string name, string password)
        {

            string queryString = $"SELECT id FROM Person (name, password) VALUES ('{name}', '{password}');";
            int id = ConnectionDB.ReturnID(queryString);

            if (id == 1)
            {

            }

           /* if (Login && mot de passe == Admin)

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

            }
            */
            
        }
        public string EnterPassword()
        {
            Console.WriteLine("Enter your password  : ");
            string password = Console.ReadLine();

            /* SecureString password = new SecureString();
            Console.WriteLine("Enter password: ");

            ConsoleKeyInfo nextKey = Console.ReadKey(true);

            while (nextKey.Key != ConsoleKey.Enter)
            {
                if (nextKey.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        // erase the last * as well
                        Console.Write(nextKey.KeyChar);
                        Console.Write(" ");
                        Console.Write(nextKey.KeyChar);
                    }
                }
                else
                {
                    password.AppendChar(nextKey.KeyChar);
                    Console.Write("*");
                }
                nextKey = Console.ReadKey(true);
            }
            password.MakeReadOnly(); */

            return password;

            

        }
    }
}