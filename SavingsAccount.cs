using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Project_2
{
    public class SavingsAccount : Account
    {
        public double Ceiling { get; set; } = 10000;
        public double Interest { get; set; } = 0.01;
        public List<Account> AllowedCreditors { get; set; } = new List<Account>();

        public SavingsAccount(string accountNumber, double amount)
        {

        }

        public static void AddInterest()
        {
            List<decimal> amounts = ConnectionDB.GetAccountColumnValues("SavingAccounts", "amount");
            List<decimal> rates = ConnectionDB.GetAccountColumnValues("SavingAccounts", "rate");


        }

   

    }
}