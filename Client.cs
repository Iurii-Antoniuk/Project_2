using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person
    { 
        public Client (string name, string password)
        {
            Name = name;
            Password = password;
            
        }

        public static void CheckAccounts(int id)
        {
            try
            {
                Console.WriteLine("Current account status: ");
                CheckCurrentAccount(id);
                Console.WriteLine("Saving accounts status: ");
                CheckSavingAccounts(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot display accounts status. " + e.Message);
            }
        }

        public static void CheckCurrentAccount(int id)
        {
            string queryString = $"SELECT * FROM CurrentAccounts WHERE id = {id};";
        }

        public static void CheckSavingAccounts(int id)
        {

        }

        public static void ImmediateTransfer(double amount)
        {
            DateTime transferDate = DateTime.Now;
            Console.WriteLine("Specify from which account you want to transfer money:");
            Console.WriteLine("Current account (c) or saving account (s)");
            string debitAccount = Console.ReadLine();

            // Récupère l'ID du client dans la propriété qui doit être définie lors de la connexion du client sur l'interface
            int debitClient_id = Client.ID;


            if (debitAccount == "c")
            {
                debitAccount = "CurrentAccounts";
                Console.WriteLine("Do you wish to transfer money to one of your saving account (s) or to an external account (e) ?");
                string creditAccount = Console.ReadLine();

                if (creditAccount == "s")
                {
                    creditAccount = "SavingAccounts";
                    // Afficher la liste de tous les comptes de sauvegarde avec leur montant. Spécifier l'id du compte epargne de qui l'opération vient.
                    string displaySavingAccounts = $"SELECT (id, amount, rate, ceiling) FROM {creditAccount} WHERE client_id = {debitClient_id}";
                    List<string> savingAccountsColumnsName = new List<string> { "id", "amount", "rate", "ceiling" };
                    ConnectionDB.SelectSQL(displaySavingAccounts, savingAccountsColumnsName);
                    Console.WriteLine("Enter the account id of the beneficiary account");
                    int creditAccount_id = Convert.ToInt32(Console.ReadLine());

                    Transaction.MoneyTransfer(debitAccount, debitClient_id, creditAccount, debitClient_id, amount, transferDate);
                }
                else if (creditAccount == "e")
                {
                    creditAccount = "CurrentAccounts";
                    Console.WriteLine("Specify the id number of the beneficiary account");
                    int externalAccount_id = Convert.ToInt32(Console.ReadLine());
                    string queryStringIdFromOtherClient = $"SELECT client_id FROM CurrentAccounts WHERE id = {externalAccount_id}";
                    int clientIdOfExternalAccount = -1;
                    clientIdOfExternalAccount = ConnectionDB.ReturnID(queryStringIdFromOtherClient);
                    if (clientIdOfExternalAccount != -1)
                    {
                        // Check if externalAccount is in DB if yes, get creditClient_id
                        try
                        {
                            Transaction.MoneyTransfer(debitAccount, debitClient_id, creditAccount, debitClient_id, amount, transferDate);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occured. " + e);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Exiting program due to input error");
                }
            }
            else if (debitAccount == "s")
            {
                debitAccount = "SavingAccounts";
                string creditAccount = "CurrentAccounts";

                Transaction.MoneyTransfer(debitAccount, debitClient_id, creditAccount, debitClient_id, amount, transferDate);
                Console.WriteLine($"{amount} has been transfered on your current accounts.");

            }
            else
            {
                Console.WriteLine("Exiting program due to input error");
            }
        }

        public static void ClientDelayedTransfer(DateTime date, double amount)
        {
            Console.WriteLine("Specify from which account you want to transfer money:");
            Console.WriteLine("Current account (c) or saving account (s)");
            string debitAccount = Console.ReadLine();

            // Récupère l'ID du client dans la propriété qui doit être définie lors de la connexion du client sur l'interface
            int debitClient_id = Client.ID;
            

            if (debitAccount == "c")
            {
                debitAccount = "CurrentAccounts";
                Console.WriteLine("Do you wish to transfer money to one of your saving account (s) or to an external account (e) ?");
                string creditAccount = Console.ReadLine();

                if (creditAccount == "s")
                {
                    creditAccount = "SavingAccounts";
                    // Afficher la liste de tous les comptes de sauvegarde avec leur montant. Spécifier l'id du compte epargne de qui l'opération vient.
                    string displaySavingAccounts = $"SELECT (id, amount, rate, ceiling) FROM {creditAccount} WHERE client_id = {debitClient_id}";
                    List<string> savingAccountsColumnsName = new List<string> { "id", "amount", "rate", "ceiling" };
                    ConnectionDB.SelectSQL(displaySavingAccounts, savingAccountsColumnsName);
                    Console.WriteLine("Enter the account id of the beneficiary account");
                    int creditAccount_id = Convert.ToInt32(Console.ReadLine());

                    DelayedTransfer.ExecuteDelayedTransfer(debitAccount, debitClient_id, creditAccount, debitClient_id, amount);
                }
                else if (creditAccount == "e")
                {
                    creditAccount = "CurrentAccounts";
                    Console.WriteLine("Specify the id number of the beneficiary account");
                    int externalAccount_id = Convert.ToInt32(Console.ReadLine());
                    string queryStringIdFromOtherClient = $"SELECT client_id FROM CurrentAccounts WHERE id = {externalAccount_id}";
                    int clientIdOfExternalAccount = -1;
                    clientIdOfExternalAccount = ConnectionDB.ReturnID(queryStringIdFromOtherClient);
                    if (clientIdOfExternalAccount != -1)
                    {
                        // Check if externalAccount is in DB if yes, get creditClient_id
                        try
                        {
                            DelayedTransfer.ExecuteDelayedTransfer(debitAccount, debitClient_id, creditAccount, clientIdOfExternalAccount,  amount);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("An error occured. " + e);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Exiting program due to input error");
                }
            }
            else if (debitAccount == "s")
            {
                debitAccount = "SavingAccounts";
                string creditAccount = "CurrentAccounts";
                DelayedTransfer.ExecuteDelayedTransfer(debitAccount, debitClient_id, creditAccount, debitClient_id, amount);
                Console.WriteLine($"{amount} has been transfered on your current accounts.");

            }
            else
            {
                Console.WriteLine("Exiting program due to input error");
            }
        }
        

        
    }

   
}
 