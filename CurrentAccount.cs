using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class CurrentAccount : Account
    {
        public double Overdraft { get; set; } = -400;
        public Client client { get; set; }

        
        public CurrentAccount(string accountNumber, double amount) 
        {
            AccountNumber = accountNumber;
            Amount = amount;
            DateTime openingDate = DateTime.Today;
        }

    }
}