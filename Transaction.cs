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


        public char ChooseDebitAccount()
        {
            Console.WriteLine("Specify from which account you want to transfer money:");
            Console.WriteLine("Current account (c) or saving account (s)");
            char debitAccount = Convert.ToChar(Console.ReadLine());

            return debitAccount;
        }

        public char ChooseRecipientAccount(int debitClient_id)
        {
            Console.WriteLine("Specify to which account you want to transfer money:");
            Console.WriteLine("Your current account (c), one of your saving account (s), or to a foreign current account (e).");
            char creditAccount = Convert.ToChar(Console.ReadLine());

                return creditAccount;
        }

        public int GetAccountIdFromAccountType(int debitClient_id, char recipientAccount)
        {
            if (recipientAccount == 'c')
            {
                int currentAccount_id = 0;
                return currentAccount_id;
            }
            else if (recipientAccount == 's')
            {
                int savingAccount_id = Transactor.GetSavingAccountIdFromClientChoice(debitClient_id);
                return savingAccount_id;
            }
            else if (recipientAccount == 'e')
            {
                Console.WriteLine("Specify the id number of the beneficiary account");
                int externalAccount_id = Convert.ToInt32(Console.ReadLine());

                int clientIdOfExternalAccount = GetClientIDFromAccountId(externalAccount_id);
                if (clientIdOfExternalAccount > 0)
                {
                    return clientIdOfExternalAccount;
                }
                else
                {
                    throw new ArgumentException("Unvalid account number");
                }
            }
            else
            {
                throw new ArgumentException("Exiting program due to input error");
            }
        }

        
        public void ExecuteTransfer(double amount, DateTime transferDate, char debitAccount, char recipientAccount, int recipientAccount_id)
        {
            
            // Récupère l'ID du client dans la propriété qui doit être définie lors de la connexion du client sur l'interface
            int debitClient_id = Person.ID;

            if (debitAccount == 'c')
            {
                TransferFromCurrentToUserChoice(debitClient_id, amount, transferDate, recipientAccount, recipientAccount_id);
            }
            else if (debitAccount == 's')
            {
                DoTransferFromSavingToCurrentAccountAccordingToDate(debitClient_id, recipientAccount_id, amount, transferDate);
            }
            else
            {
                Console.WriteLine("Exiting program due to input error");
            }
        }


        public void TransferFromCurrentToUserChoice(int debitClient_id, double amount, DateTime transferDate, char recipientAccount, int recipientAccount_id)
        {
            if (recipientAccount == 's')
            {
                //int SavingAccount_id = Transactor.GetSavingAccountIdFromClientChoice(debitClient_id);
                DoTransferFromCurrentAccountToSavingAccountAccordingToDate(debitClient_id, recipientAccount_id, amount, transferDate);
            }
            else if (recipientAccount == 'e')
            {
                /*Console.WriteLine("Specify the id number of the beneficiary account");
                int externalAccount_id = Convert.ToInt32(Console.ReadLine());*/

                int clientIdOfExternalAccount = GetClientIDFromAccountId(recipientAccount_id);
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

        public int GetClientIDFromAccountId(int account_id)
        {
            string queryStringIdFromOtherClient = $"SELECT client_id FROM CurrentAccounts WHERE id = {account_id}";
            int clientIdOfExternalAccount = ConnectionDB.ReturnID(queryStringIdFromOtherClient);

            if (clientIdOfExternalAccount > 0)
            {
                return clientIdOfExternalAccount;
            }
            else
            {
                return 0;
            }
        }
    }
}