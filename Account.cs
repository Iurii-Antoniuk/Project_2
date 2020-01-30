using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public double Amount { get; set; }
        public DateTime OpeningDate { get; set; }

        public Account (int accountNumber, double amount)
        {
            AccountNumber = accountNumber;
            OpeningDate = DateTime.Today;
            if (amount < 10)
            {
                Console.WriteLine("Invalid amount");
                //Terminer la condition: empêcher l'instanciation si montant trop faible
            }
            else
            {
                Amount = amount;
            }
        }
    }
}