using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Project_2
{
    public class Person
    {
        protected static string Name { get; set; }
        private static int ID { get; set; }
        protected static string Password { get; set; }     
        
        public string PasswordGenerator()
        {
            string caracteres = "azertyuiopqsdfghjklmwxcvbn1234567890";
            Random caracteAlea = new Random();
            
            string password = "";
            for (int i = 0; i < 4; i++) // 4 caracteres
            {
                int majOrMin = caracteAlea.Next(2);
                string carac = caracteres[caracteAlea.Next(0, caracteres.Length)].ToString();
                if (majOrMin == 0)
                {
                    password += carac.ToUpper();
                }
                else
                {
                    password += carac.ToLower();
                }
            }
            return Password = password;
        }
        public static string CryptPassword(string password)
        {
            byte[] encodPassword = Encoding.ASCII.GetBytes(password);
            encodPassword = new SHA256Managed().ComputeHash(encodPassword);
            string cryptPassword = Encoding.ASCII.GetString(encodPassword);
            return cryptPassword;
        }

    }
}