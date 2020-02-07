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

        public static void MoneyTransfer(string debitAccount, int debitClient_id, string creditAccount, int creditClient_id, double amount, DateTime date)
        {
            string queryString = $"UPDATE {debitAccount} SET amount = ((SELECT amount FROM {debitAccount} WHERE client_id = {debitClient_id}) - {amount}) WHERE client_id = {debitClient_id} " +
                                 $"UPDATE {creditAccount} SET amount = ((SELECT amount FROM {creditAccount} WHERE client_id = {creditClient_id}) + {amount}) WHERE client_id = {creditClient_id} " +
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiaryAccount_id, amount, 'date') " +
                                 $"VALUES(" +
                                 $"(SELECT id FROM {debitAccount} WHERE client_id = {debitClient_id}), " +
                                 $"'Money Transfer', " +
                                 $"(SELECT id FROM {creditAccount} WHERE client_id = {creditClient_id}), " +
                                 $"{amount}, " +
                                 $"\"{date}\");";

            ConnectionDB.NonQuerySQL(queryString);
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