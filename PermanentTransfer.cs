using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project_2
{
    public class PermanentTransfer : Transaction
    {
        
        public void ExecutePermanentTransfer(double amount)
        {
            int debitClient_id = Person.ID;

            Console.WriteLine("First date of transfer. ");
            DateTime FirstExecution = Transactor.GetCheckedDate();
            Console.WriteLine("Last date of transfer. ");
            DateTime LastExecution = Transactor.GetCheckedDate();

            while (LastExecution <= FirstExecution || FirstExecution < DateTime.Today)
            {
                Console.WriteLine("Unvalid dates");
                Console.WriteLine("First date of transfer: ");
                FirstExecution = Transactor.GetCheckedDate();
                Console.WriteLine("Last date of transfer: ");
                LastExecution = Transactor.GetCheckedDate();
            }
            
            Console.WriteLine("Give the periodicity (number of days between the transfers) ");
            int interval = Math.Abs(Convert.ToInt32(Console.ReadLine()));
            TimeSpan Interval = new TimeSpan(interval, 0, 0, 0);

            char debitAccount = ChooseDebitAccount();
            int debitSavingAccount_id = 0;
            char recipientAccount = 'a';
            int recipientAccount_id = 0;
            if (debitAccount == 's')
            {
                debitSavingAccount_id = Transactor.GetSavingAccountIdFromClientChoice(Person.ID);
            }
            else
            {
                recipientAccount = ChooseRecipientAccount(debitClient_id);
                recipientAccount_id = GetAccountIdFromAccountType(debitClient_id, recipientAccount);
            }
            while (FirstExecution < LastExecution && FirstExecution >= DateTime.Today)
            {
                // Voir pour sortir execute transfer de la boucle et mettre cette boucle plus tard afin de pouvoir sélectionner
                // en amont les comptes de départ et d'arrivée.
                if (FirstExecution == DateTime.Today)
                {
                    ExecuteTransfer(amount, FirstExecution, debitAccount, debitSavingAccount_id, recipientAccount, recipientAccount_id);
                    FirstExecution = FirstExecution.Add(Interval);
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
                    Task.Delay(TimeSpan.FromHours(24));
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
                        Task.Delay(TimeSpan.FromHours(24));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured. " + e);
            }
        }

        public override void DoTransferFromSavingToCurrentAccountAccordingToDate(int debitClient_id, int SavingAccount_id, int recipientAccount_id, double amount, DateTime transferDate)
        {
            while (DateTime.Today <= transferDate)
            {
                if (DateTime.Today == transferDate)
                {
                    Transactor.TransferFromSavingToCurrentAccount(debitClient_id, SavingAccount_id, amount);
                    Task.Delay(TimeSpan.FromHours(24));
                }
            }
        }
    }

    
}