using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{

    public enum AccountType
    {
        Saving = 1,
        Current = 2
    }

    public class Account
    {
        public string AccountNumber { get; set; }
        public double Amount { get; set; }
        protected DateTime OpeningDate;

    }
}