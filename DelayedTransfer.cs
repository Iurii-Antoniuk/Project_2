using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class DelayedTransfer : Transaction
    {
        public static void ExecuteDelayedTransfer(string debitAccount, int debitClient_id, string creditAccount, int creditClient_id, double amount)
        {
            Console.WriteLine("Give the transfer execution date (accepted format YYYY-MM-DD) : ");
            DateTime executionDate = GetDate();

            while (DateTime.Now <= executionDate)
            {
                if (DateTime.Now == executionDate)
                {
                    MoneyTransfer(debitAccount, debitClient_id, creditAccount, creditClient_id, amount, executionDate);
                    /*string queryString = $"INSERT INTO [Transaction] (currentAccount_id, transactionType, beneficiaryAccount_id, amount, [date])" + 
                                            $"VALUES("+
                                            $"(SELECT id FROM {debitAccount} WHERE client_id = {debitClient_id}),"+
                                            $"'Money Transfer'," +
                                            $"(SELECT id FROM {creditAccount} WHERE client_id = {creditClient_id}),"+
                                            $"{amount},"+
                                            $"{executionDate});";
                    ConnectionDB.NonQuerySQL(queryString);*/
                }
            }
        }
    }
}