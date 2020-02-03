using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class DelayedTransfer : Transaction
    {

        public static void ExecutePermanentTransfer(CurrentAccount debitAccount, CurrentAccount creditAccount, double amount)
        {
            Console.WriteLine("Give the transfer execution date (accepted format YYYY-MM-DD) : ");
            DateTime executionDate = GetDate();

            while (DateTime.Now <= executionDate)
            {
                if (DateTime.Now == executionDate)
                {
                    MoneyTransfer(debitAccount, creditAccount, amount);
                }
            }

            

        }
    }
}