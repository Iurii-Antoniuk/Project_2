using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class InstantTransfer : Transaction
    { 

        public static void DoImmediateTransfer(string debitAccount, int debitClient_id, string creditAccount, int creditClient_id, double amount)
        {
            DateTime executionDate = DateTime.Now;
            MoneyTransfer(debitAccount, debitClient_id, creditAccount, creditClient_id, amount, executionDate);
        }
    }
}