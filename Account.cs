using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Account
    {
        public static int AccountNumber { get; set; }
        public static double Amount { get; set; }
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

        public static void WithdrawMoney(Account accounts, double amount)
        {
            Console.WriteLine("There are : " + Amount + " of money from " + AccountNumber);
            double money = Amount - amount;

            Amount = money;

            Console.WriteLine("You have withdraw " + amount + " euros.");
            Console.WriteLine("There are : " + Amount + " of money left");
        }
    }
}