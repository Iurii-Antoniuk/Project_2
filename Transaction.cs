using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Transaction
    {
        protected Account ActualAccount { get; set; }
        protected DateTime DateTransaction { get; set; }
        protected double Amount { get; set; }
        protected Account DestinationAccount { get; set; }

        public static void MoneyTransfer(Account DebitAccount, Account CreditAccount, double amount)
        {
            Console.WriteLine("Money on debit account before transfer: " + DebitAccount.Amount);
            Console.WriteLine("Money on credit account before transfer: " + CreditAccount.Amount);
                     
            DebitAccount.Amount = DebitAccount.Amount - amount;
            CreditAccount.Amount = CreditAccount.Amount + amount;

            DateTime date = DateTime.Today;

            Console.WriteLine("Money on debit account after transfer: " + DebitAccount.Amount);
            Console.WriteLine("Money on credit account after transfer: " + CreditAccount.Amount);

        }

        //voir pour ajouter une méthode pour transférer de l'argent vers des comptes d'autres users (liste bénificiaires)

        public static void WithdrawMoney(Account account, double amount)
        {
            Console.WriteLine("There are : " + account.Amount + " of money from " + account.AccountNumber);

            account.Amount = account.Amount - amount;

            Console.WriteLine("You have withdraw " + amount + " euros.");
            Console.WriteLine("There are : " + account.Amount + " of money left");
        }

        public static void AddMoney(Account account, double amount)
        {
            Console.WriteLine("There are : " + account.Amount + " of money from " + account.AccountNumber);

            account.Amount = account.Amount + amount;

            Console.WriteLine("You have withdraw " + amount + " euros.");
            Console.WriteLine("There are : " + account.Amount + " of money left");
        }

        public static DateTime GetDate()
        {
            string input = Console.ReadLine();
            DateTime firstExecution;
            while (!DateTime.TryParse(input, out firstExecution))
            {
                Console.WriteLine("The format of the time is not right");
                Console.WriteLine("Give good format (accepted format YYYY-MM-DD) of date :");
                input = Console.ReadLine();
            }
            return firstExecution;
        }
    }
}