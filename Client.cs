using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person
    { 
        public void CheckCurrentAccounts()
        {
            Console.WriteLine("Current Accounts : ");
            int client_id = ID;
            string queryString = $"SELECT id, amount, overdraft, openingDate FROM CurrentAccounts WHERE client_id = '{client_id}';";
            List<string> currentAccountInfo = new List<string> { "id", "amount", "overdraft","openingDate" };
            foreach(string item in currentAccountInfo)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, currentAccountInfo);
        }
        public void CheckSavingAccounts()
        {
            Console.WriteLine("Savings Accounts : ");
            int client_id = ID;
            string queryString = $"SELECT id, amount, rate, ceiling, openingDate FROM SavingAccounts WHERE client_id = '{client_id}';";
            List<string> savingAccountInfo = new List<string> { "id", "amount", "rate", "ceiling", "openingDate" };
            foreach (string item in savingAccountInfo)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, savingAccountInfo);
        }
        
        public void WithdrawMoney(double amount)
        {
            int client_id = ID;
            string queryString1 = $"SELECT amount FROM CurrentAccounts WHERE client_id={ client_id};";
            decimal currentAmount = ConnectionDB.ReturnDecimal(queryString1);

            string queryString2 = $"SELECT overdraft FROM CurrentAccounts WHERE client_id={ client_id};";
            decimal overdraft = ConnectionDB.ReturnDecimal(queryString2);

            string queryString3 = $"SELECT id FROM CurrentAccounts WHERE client_id={ client_id};";
            int currentAccountID = ConnectionDB.ReturnID(queryString3);

            if (Convert.ToDouble(currentAmount - overdraft) >= amount)
            {
                DateTime dateOp = DateTime.Now;
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - {amount}) WHERE  client_id = {  client_id }; " +
                                     $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, amount, \"date\") " +
                                     $"VALUES({currentAccountID}, 'withdraw', {amount}, '{dateOp}')";
                ConnectionDB.NonQuerySQL(queryString);
            }
            else
            {
                Console.WriteLine("Not enough money on current account.");
            }
        }

        public void ImmediateTransfer(double amount)
        {
            DateTime transferDate = DateTime.Now;
            Console.WriteLine("Specify from which account you want to transfer money:");
            Console.WriteLine("Current account (c) or saving account (s)");
            string debitAccount = Console.ReadLine();

            // Récupère l'ID du client dans la propriété qui doit être définie lors de la connexion du client sur l'interface
            int debitClient_id = Client.ID;
            Console.WriteLine(debitClient_id);

            if (debitAccount == "c")
            {
                debitAccount = "CurrentAccounts";
                Console.WriteLine("Do you wish to transfer money to one of your saving account (s) or to an external account (e) ?");
                string creditAccount = Console.ReadLine();

                if (creditAccount == "s")
                {
                    creditAccount = "SavingAccounts";
                    // Afficher la liste de tous les comptes de sauvegarde avec leur montant. Spécifier l'id du compte epargne de qui l'opération vient.
                    // Mais on a deja la methode CheckSavingAccounts qui fait la meme chose!..?
                    // "Spécifier l'id du compte epargne de qui l'opération vient." -ahhh???
                    string displaySavingAccounts = $"SELECT id, amount, ceiling FROM SavingAccounts WHERE client_id = {debitClient_id}";
                    List<string> savingAccountsColumnsName = new List<string> { "id", "amount", "ceiling" };
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
 