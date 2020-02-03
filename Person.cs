using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public abstract class Person
    {
        protected static string Name { get; set; }
        protected static string ID { get; set; }
        protected static string Password { get; set; }


        
        

        public string PasswordGenerator()
        {
            string caracteres = "azertyuiopqsdfghjklmwxcvbn1234567890";
            Random caracteAlea = new Random();
            
            string password = "";
            for (int i = 0; i < 8; i++) // 8 caracteres
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

        public string IdGenerator()
        {
            ID = Guid.NewGuid().ToString("N").Substring(0, 12);
            // generate a unique id (length = 12)
            return ID;
        }

    }
}