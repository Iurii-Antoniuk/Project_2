using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Security;
using System.Runtime.InteropServices;

namespace Project_2
{
    public class Authentification
    {
        public void Login()
        {
            Console.WriteLine("Enter your name : ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your password : ");
            SecureString passwordHide = GetPassword();

            IntPtr bstr = Marshal.SecureStringToBSTR(passwordHide);
            string password = Marshal.PtrToStringBSTR(bstr);

            password = Person.CryptPassword(password);
            string queryString = $"SELECT id FROM Person WHERE name = '{name}' AND password ='{password}';";
            int id = ConnectionDB.ReturnID(queryString);
            if (id == 1)
            {
                Console.WriteLine("Hello admin");
                Person.ID = id;
            }
            else if (id > 1)
            {
                Console.WriteLine("Hello user");
                Person.ID = id;
            }
        }
        public SecureString GetPassword()
        {
            SecureString password = new SecureString();
            ConsoleKeyInfo nextKey = Console.ReadKey(true);
            while (nextKey.Key != ConsoleKey.Enter)
            {
                if (nextKey.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
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
            Console.WriteLine();
            password.MakeReadOnly();
            return password;
        }
        public void ModifyPassword(int ID)
        {
            Console.WriteLine("Enter your password : ");
            SecureString passwordHide = GetPassword();
            IntPtr bstr = Marshal.SecureStringToBSTR(passwordHide);
            string password = Marshal.PtrToStringBSTR(bstr);

            password = Person.CryptPassword(password);
            string queryString = $"SELECT id FROM Person WHERE id = '{ID}' AND password ='{password}';";
            int id = ConnectionDB.ReturnID(queryString);
            if (ID == id)
            {
                Console.WriteLine("Enter your new password : ");
                SecureString newpasswordHide = GetPassword();
                IntPtr newbstr = Marshal.SecureStringToBSTR(newpasswordHide);
                string newpassword = Marshal.PtrToStringBSTR(newbstr);

                newpassword = Person.CryptPassword(newpassword);
                string newqueryString = $"UPDATE Person SET password = '{newpassword}' WHERE id = '{ID}';";
                ConnectionDB.NonQuerySQL(newqueryString);
            }
            else
            {
                Console.WriteLine("password is wrong");
            }
        }
    }
}
