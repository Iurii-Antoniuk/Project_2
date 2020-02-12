using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class SavingsAccount : Account
    {
        public decimal Ceiling { get; set; }
        public decimal Interest { get; set; }
        public List<Account> AllowedCreditors { get; set; } = new List<Account>();

        public void CreateSavingAccount(int client_id, double amount)
        {
            Console.WriteLine("Enter the rate (decimal) of the new account : ");
            Interest = Convert.ToDecimal(Console.ReadLine());
            
            Console.WriteLine("Enter the maximum amount you can have on the new account : ");
            Ceiling = Convert.ToDecimal(Console.ReadLine());

            DateTime openingDate = DateTime.Now;

            string queryString = $"INSERT INTO SavingAccounts (client_id, amount, rate, ceiling, openingDate) " +
                                $" VALUES ('{client_id}', '{amount}','{Interest}','{Ceiling}','{openingDate}');";
            ConnectionDB.NonQuerySQL(queryString);
        }

        public void DeleteSavingAccount(int account_id)
        {
            string queryString = $"DELETE FROM SavingAccounts WHERE id ='{account_id}';";
            ConnectionDB.NonQuerySQL(queryString);
        }

    }
}