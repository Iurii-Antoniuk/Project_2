using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class PermanentTransfer : Transaction
    {
        
        public static void ExecutePermanentTransfer(CurrentAccount debitAccount, CurrentAccount creditAccount, double amount)
        {
            Console.WriteLine("Give the first date of the transfer (accepted format YYYY-MM-DD) : ");
            DateTime FirstExecution = GetDate();
            Console.WriteLine("Give the first date of the transfer (accepted format YYYY-MM-DD) : ");
            DateTime LastExecution = GetDate();
            Console.WriteLine("Give the periodicity (number of days between the transfers) ");
            int interval = Convert.ToInt32(Console.ReadLine());
            TimeSpan Interval = new TimeSpan(interval, 0, 0, 0);

            while (FirstExecution < LastExecution && FirstExecution >= DateTime.Now)
            {
                if (FirstExecution == DateTime.Now)
                {
                    MoneyTransfer(debitAccount, creditAccount, amount);
                    FirstExecution += Interval;
                }
                
            }
        }
         
    }

    
}