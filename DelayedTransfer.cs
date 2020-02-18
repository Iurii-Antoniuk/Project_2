using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class DelayedTransfer : Transaction
    {

        public void ExecuteDelayedTransfer(double amount)
        {
            int debitClient_id = Person.ID;
            Console.WriteLine("Enter the first transfer execution date (format YYYY-MM-DD): ");
            DateTime transferDate = Transactor.GetCheckedDate();

            if (transferDate < DateTime.Today)
            {
                Console.WriteLine("Entered date is already past. Please enter a valid date.");
            }
            else
            {
                char debitAccount = ChooseDebitAccount();
                char recipientAccount = ChooseRecipientAccount(debitClient_id);
                int recipientAccount_id = GetAccountIdFromAccountType(debitClient_id, recipientAccount);
                ExecuteTransfer(amount, transferDate, debitAccount, recipientAccount, recipientAccount_id);
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


        /*public static void ExecuteDelayedTransfer(double amount)
        {
            Console.WriteLine("Enter the first transfer execution date (format YYYY-MM-DD): ");
            DateTime transferDate = Transaction.GetCheckedDate();
            if (transferDate < DateTime.Today)
            {
                Console.WriteLine("Entered date is already past. Please enter a valid date.");
            }
            else
            {
                Console.WriteLine("Specify from which account you want to transfer money:");
                Console.WriteLine("Current account (c) or saving account (s)");
                string debitAccount = Console.ReadLine();

                // Récupère l'ID du client dans la propriété qui doit être définie lors de la connexion du client sur l'interface
                int debitClient_id = 3;


                if (debitAccount == "c")
                {
                    debitAccount = "CurrentAccounts";
                    Console.WriteLine("Do you wish to transfer money to one of your saving account (s) or to an external account (e) ?");
                    string creditAccount = Console.ReadLine();

                    if (creditAccount == "s")
                    {
                        int SavingAccount_id = Transaction.GetSavingAccountIdFromClientChoice(debitClient_id);
                        while (DateTime.Today <= transferDate)
                        {
                            if (DateTime.Today == transferDate)
                            {
                                Transaction.TransferFromCurrentAccountToSavingAccount(debitClient_id, SavingAccount_id, amount, transferDate);
                                break;
                            }
                        }
                    }
                    else if (creditAccount == "e")
                    {
                        creditAccount = "CurrentAccounts";
                        Console.WriteLine("Specify the id number of the beneficiary account");
                        int externalAccount_id = Convert.ToInt32(Console.ReadLine());

                        // Check if externalAccount is in DB if yes, get creditClient_id
                        string queryStringIdFromOtherClient = $"SELECT client_id FROM CurrentAccounts WHERE id = {externalAccount_id}";
                        int clientIdOfExternalAccount = ConnectionDB.ReturnID(queryStringIdFromOtherClient);

                        if (clientIdOfExternalAccount > 0)
                        {
                            try
                            {
                                while (DateTime.Today <= transferDate)
                                {
                                    if (DateTime.Today == transferDate)
                                    {
                                        Transaction.TransferFromCurrentToCurrentAccount(debitClient_id, clientIdOfExternalAccount, amount, transferDate);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("An error occured. " + e);
                            }
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
                else if (debitAccount == "s")
                {
                    int SavingAccount_id = Transaction.GetSavingAccountIdFromClientChoice(debitClient_id);
                    while (DateTime.Today <= transferDate)
                    {
                        if (DateTime.Today == transferDate)
                        {
                            Transaction.TransferFromSavingToCurrentAccount(debitClient_id, SavingAccount_id, amount, transferDate);
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Exiting program due to input error");
                }
            }  
        }*/
    }
}