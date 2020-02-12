using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class CurrentAccount : Account
    {
        public decimal Overdraft { get; set; }
        public Client Client { get; set; }

        
        /*public CurrentAccount(string accountNumber, double amount) 
        {
            AccountNumber = accountNumber;
            Amount = amount;
            DateTime openingDate = DateTime.Today;
        }*/

        public void CreateCurrentAccount(int client_id, double amount)
        {
            Console.WriteLine("Enter the amount of the overdraft of the new account : ");
            Overdraft = Convert.ToDecimal(Console.ReadLine());

            DateTime openingDate = DateTime.Now;

            string queryString = $"INSERT INTO CurrentAccounts (client_id, amount, overdraft, openingDate) " +
                                $" VALUES ('{client_id}', '{amount}','{Overdraft}','{openingDate}');";
            ConnectionDB.NonQuerySQL(queryString);
        }

    }
}