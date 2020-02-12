using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Project_2
{
    public abstract class Transaction
    {
        protected Account ActualAccount { get; set; }
        protected DateTime DateTransaction { get; set; }
        protected double Amount { get; set; }
        protected Account DestinationAccount { get; set; }

        public abstract void DoTransferFromCurrentAccountToSavingAccountAccordingToDate(int debitClient_id, int SavingAccount_id, double amount, DateTime transferDate);
        public abstract void DoTransferFromCurrentToOtherCurrentAccountAccordingToDate(int debitClient_id, int clientIdOfExternalAccount, double amount, DateTime transferDate);
        public abstract void DoTransferFromSavingToCurrentAccountAccordingToDate(int debitClient_id, int SavingAccount_id, double amount, DateTime transferDate);

        
        public void ExecuteTransfer(double amount, DateTime transferDate)
        {
            Console.WriteLine("Specify from which account you want to transfer money:");
            Console.WriteLine("Current account (c) or saving account (s)");
            char debitAccount = Convert.ToChar(Console.ReadLine());

            // Récupère l'ID du client dans la propriété qui doit être définie lors de la connexion du client sur l'interface
            int debitClient_id = 2;

            if (debitAccount == 'c')
            {
                TransferFromCurrentToUserChoice(debitClient_id, amount, transferDate);
            }
            else if (debitAccount == 's')
            {
                int SavingAccount_id = Transactor.GetSavingAccountIdFromClientChoice(debitClient_id);
                DoTransferFromSavingToCurrentAccountAccordingToDate(debitClient_id, SavingAccount_id, amount, transferDate);
            }
            else
            {
                Console.WriteLine("Exiting program due to input error");
            }
        }


        public void TransferFromCurrentToUserChoice(int debitClient_id, double amount, DateTime transferDate)
        {
            Console.WriteLine("Do you wish to transfer money to one of your saving account (s) or to an external account (e) ?");
            char creditAccount = Convert.ToChar(Console.ReadLine());

            if (creditAccount == 's')
            {
                int SavingAccount_id = Transactor.GetSavingAccountIdFromClientChoice(debitClient_id);
                DoTransferFromCurrentAccountToSavingAccountAccordingToDate(debitClient_id, SavingAccount_id, amount, transferDate);
            }
            else if (creditAccount == 'e')
            {
                Console.WriteLine("Specify the id number of the beneficiary account");
                int externalAccount_id = Convert.ToInt32(Console.ReadLine());

                // Check if externalAccount is in DB if yes, get creditClient_id
                string queryStringIdFromOtherClient = $"SELECT client_id FROM CurrentAccounts WHERE id = {externalAccount_id}";
                int clientIdOfExternalAccount = ConnectionDB.ReturnID(queryStringIdFromOtherClient);

                if (clientIdOfExternalAccount > 0)
                {
                    DoTransferFromCurrentToOtherCurrentAccountAccordingToDate(debitClient_id, clientIdOfExternalAccount, amount, transferDate);
                }
                else
                {
                    Console.WriteLine("Please specify a valid recipient account number.");
                }
            }
            else
            {
                Console.WriteLine("Exiting program due to input error");
            }
        }
    }
}