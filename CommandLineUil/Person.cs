﻿using System;
using System.Text;
using System.Security.Cryptography;

namespace IKEACmdUtil
{
    public class Person
    {
        public static int ID { get; set; }
        public static string Password { get; set; }
        
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
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            SHA256 hasher = SHA256.Create();
            byte[] encryptedPasswordBytes = hasher.ComputeHash(passwordBytes);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < encryptedPasswordBytes.Length; i++)
            {
                builder.Append(encryptedPasswordBytes[i].ToString("x2"));
            }

            String encryptedPassword = builder.ToString();
            return encryptedPassword;
        }   
    }
}