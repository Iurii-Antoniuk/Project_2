using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class DelayedTransfer : Transaction
    {
        public static void ExecuteDelayedTransfer(double amount)
        {
            Console.WriteLine("Enter the first transfer execution date (format YYYY-MM-DD): ");
            DateTime transferDate = Transaction.CheckDate();
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
        }
    }
}