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

        public void CreateCurrentAccount(int client_id, double amount, decimal overdraft)
        {
            DateTime openingDate = DateTime.Today;

            string queryString = $"INSERT INTO CurrentAccounts (client_id, amount, overdraft, openingDate) " +
                                $" VALUES ({client_id}, {amount}, {overdraft}, '{openingDate}');";
            ConnectionDB.NonQuerySQL(queryString);
        }

    }
}