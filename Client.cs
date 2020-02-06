using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person
    { 
        /*
         * public Client (string name)
        {
            Name = name;
            
        } */

        /*
        public void CheckAccounts(int id)
        {
            try
            {
                Console.WriteLine("Current account status: ");
                //CheckCurrentAccount(id);
                Console.WriteLine("Saving accounts status: ");
                CheckSavingAccounts(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot display accounts status. " + e.Message);
            }
        }
        */
        public void CheckCurrentAccount(int client_id)
        {
            string queryString = $"SELECT id, amount, overdraft, openingDate FROM CurrentAccounts WHERE client_id = '{client_id}';";
            List<string> currentAccountInfo = new List<string> { "id", "amount", "overdraft","openingDate" };
            ConnectionDB.SelectSQL(queryString, currentAccountInfo);
        }

        public void CheckSavingAccounts(int client_id)
        {
            string queryString = $"SELECT id, amount, rate, ceiling, openingDate FROM SavingAccounts WHERE client_id = '{client_id}';";
            List<string> savingAccountInfo = new List<string> { "id", "amount", "rate", "ceiling", "openingDate" };
            ConnectionDB.SelectSQL(queryString, savingAccountInfo);
        }
        

        
    }

   
}
 