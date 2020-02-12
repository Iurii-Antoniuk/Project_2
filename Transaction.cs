using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Project_2
{
    public class Transaction
    {
        protected Account ActualAccount { get; set; }
        protected DateTime DateTransaction { get; set; }
        protected double Amount { get; set; }
        protected Account DestinationAccount { get; set; }

        public static void TransferFromCurrentToCurrentAccount(int debitClient_id, int creditClient_id, double amount, DateTime date)
        {
            string queryString = $"UPDATE CurrentAccounts SET amount = (amount - {amount}) WHERE client_id = {debitClient_id} " +
                                 $"UPDATE CurrentAccounts SET amount = (amount + {amount}) WHERE client_id = {creditClient_id} " +
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiaryAccount_id, amount, \"date\") " +
                                 $"VALUES(" +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"\'Money Transfer\', " +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {creditClient_id}), " +
                                 $"{amount}, " +
                                 $"\'{date}\');";
            ConnectionDB.NonQuerySQL(queryString);
        }

        public static void TransferFromSavingToCurrentAccount(int debitClient_id, int SavingAccount_id, double amount, DateTime date)
        {
            /*Client client = new Client();
            client.CheckSavingAccounts(debitClient_id);
            Console.WriteLine("Specify the saving account id you want to transfer the money from.");
            int SavingAccount_id = Convert.ToInt32(Console.ReadLine());*/


            string queryString = $"UPDATE SavingAccounts SET amount = (amount - {amount}) WHERE id = {SavingAccount_id} " +
                                 $"UPDATE CurrentAccounts SET amount = (amount + {amount}) WHERE client_id = {debitClient_id}; " +
                                 $"INSERT INTO \"Transaction\" (SavingAccount_id, transactionType, beneficiaryAccount_id, amount, \"date\") " +
                                 $"VALUES(" +
                                 $"(SELECT id FROM SavingAccounts WHERE id = {SavingAccount_id}), " +
                                 $"\'Money Transfer\', " +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"{amount}, " +
                                 $"\'{date}\');";
            ConnectionDB.NonQuerySQL(queryString);
        }

        public static int GetSavingAccountIdFromClientChoice(int debitClient_id)
        {
            Client client = new Client();
            client.CheckSavingAccounts(debitClient_id);
            Console.WriteLine("Specify the saving account id for the money transfer. ");
            int SavingAccount_id = Convert.ToInt32(Console.ReadLine());

            return SavingAccount_id;
        }

        public static void TransferFromCurrentAccountToSavingAccount(int debitClient_id, int SavingAccount_id, double amount, DateTime date)
        {
/*
            Client client = new Client();
            client.CheckSavingAccounts(debitClient_id);
            Console.WriteLine("Specify the saving account id to transfer the money to.");
            int SavingAccount_id = Convert.ToInt32(Console.ReadLine());*/

            string queryString = $"UPDATE CurrentAccounts SET amount = (amount - {amount}) WHERE client_id = {debitClient_id} " +
                                 $"UPDATE SavingAccounts SET amount = (amount + {amount}) WHERE id = {SavingAccount_id} " +
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, SavingAccount_id, amount, \"date\") " +
                                 $"VALUES(" +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"\'Money Transfer\', " +
                                 $"(SELECT id FROM SavingAccounts WHERE id = {SavingAccount_id}), " +
                                 $"{amount}, " +
                                 $"\'{date}\');";
            ConnectionDB.NonQuerySQL(queryString);
        }



        /*public static DateTime GetDate()
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
        }*/

        public static DateTime CheckDate()
        {
            Console.WriteLine("Enter a date (accepted format DD/MM/YYYY): ");
            string input = Console.ReadLine();
            CultureInfo fr = new CultureInfo("fr");
            DateTime dateValue;
            while(!DateTime.TryParseExact(input, "d", fr, DateTimeStyles.AllowLeadingWhite, out dateValue))
            {
                Console.WriteLine("The format of the time is not right");
                Console.WriteLine("Give good format (accepted format DD/MM/YYYY) of date :");
                input = Console.ReadLine();
            }
            Console.WriteLine("Entered date: " + dateValue.ToString("d"));

            return dateValue;
        }
    }
}