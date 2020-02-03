using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class SavingsAccount : Account
    {
        public double Ceiling { get; set; }
        public double Interest { get; set; }
        public List<Account> AllowedCreditors { get; set; } = new List<Account>();

        public SavingsAccount(int accountNumber, double amount) : base(accountNumber, amount)
        {

        }

    }
}