using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person
    { 
        public Client (string name, string password)
        {
            Name = name;
            Password = password;
            
        }

        public static void CheckAccounts(int id)
        {
            try
            {
                Console.WriteLine("Current account status: ");
                CheckCurrentAccount(id);
                Console.WriteLine("Saving accounts status: ");
                CheckSavingAccounts(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot display accounts status. " + e.Message);
            }
        }

        public static void CheckCurrentAccount(int id)
        {
            string queryString = $"SELECT * FROM CurrentAccounts WHERE id = {id};";
            ConnectionDB.SelectSQL
        }

        public static void CheckSavingAccounts(int id)
        {

        }
        

        
    }

   
}
 