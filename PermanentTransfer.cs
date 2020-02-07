using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class PermanentTransfer : Transaction
    {
        
        public static void ExecutePermanentTransfer(string debitAccount, int debitClient_id, string creditAccount, int creditClient_id, double amount)
        {
            Console.WriteLine("Give the first date of the transfer (accepted format YYYY-MM-DD) : ");
            DateTime FirstExecution = GetDate();
            Console.WriteLine("Give the last date of the transfer (accepted format YYYY-MM-DD) : ");
            DateTime LastExecution = GetDate();
            Console.WriteLine("Give the periodicity (number of days between the transfers) ");
            int interval = Convert.ToInt32(Console.ReadLine());
            TimeSpan Interval = new TimeSpan(interval, 0, 0, 0);

            while (FirstExecution < LastExecution && FirstExecution >= DateTime.Now)
            {
                if (FirstExecution == DateTime.Now)
                {
                    DateTime todayDate = DateTime.Now;
                    Transaction.MoneyTransfer(debitAccount, debitClient_id, creditAccount, creditClient_id, amount, todayDate);
                    FirstExecution += Interval;
                }
                
            }
        }
         
    }

    
}