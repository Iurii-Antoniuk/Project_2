using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class PermanentTransfer : Transaction
    {
        
        public void ExecutePermanentTransfer(double amount)
        {
            Console.WriteLine("First date of transfer. ");
            DateTime FirstExecution = Transactor.GetCheckedDate();
            Console.WriteLine("Last date of transfer. ");
            DateTime LastExecution = Transactor.GetCheckedDate();
            Console.WriteLine("Give the periodicity (number of days between the transfers) ");
            int interval = Convert.ToInt32(Console.ReadLine());
            TimeSpan Interval = new TimeSpan(interval, 0, 0, 0);

            while (FirstExecution < LastExecution && FirstExecution >= DateTime.Now)
            {
                if (FirstExecution == DateTime.Now)
                {
                    ExecuteTransfer(amount, FirstExecution);
                    FirstExecution += Interval;
                }
            }
        }

        public override void DoTransferFromCurrentAccountToSavingAccountAccordingToDate(int debitClient_id, int SavingAccount_id, double amount, DateTime transferDate)
        {

            while (DateTime.Today <= transferDate)
            {
                if (DateTime.Today == transferDate)
                {
                    Transactor.TransferFromCurrentAccountToSavingAccount(debitClient_id, SavingAccount_id, amount);
                    break;
                }
            }
        }

        public override void DoTransferFromCurrentToOtherCurrentAccountAccordingToDate(int debitClient_id, int clientIdOfExternalAccount, double amount, DateTime transferDate)
        {
            try
            {
                while (DateTime.Today <= transferDate)
                {
                    if (DateTime.Today == transferDate)
                    {
                        Transactor.TransferFromCurrentToCurrentAccount(debitClient_id, clientIdOfExternalAccount, amount);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured. " + e);
            }
        }

        public override void DoTransferFromSavingToCurrentAccountAccordingToDate(int debitClient_id, int SavingAccount_id, double amount, DateTime transferDate)
        {
            while (DateTime.Today <= transferDate)
            {
                if (DateTime.Today == transferDate)
                {
                    Transactor.TransferFromSavingToCurrentAccount(debitClient_id, SavingAccount_id, amount);
                    break;
                }
            }
        }

    }

    
}