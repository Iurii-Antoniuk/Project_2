using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class CurrentAccount : Account
    {
        public double Overdraft {get; set;}
        public Client client { get; set; }
        
        public CurrentAccount(int accountNumber, double amount) : base (accountNumber, amount)
        {

        }

    }
}