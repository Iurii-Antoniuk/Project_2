using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person
    {
        public void CheckCurrentAccount(int client_id)
        {
            string queryString = $"SELECT id, amount, overdraft, openingDate FROM CurrentAccounts WHERE client_id = '{client_id}';";
            List<string> currentAccountInfo = new List<string> { "id", "amount", "overdraft", "openingDate" };
            foreach (string item in currentAccountInfo)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, currentAccountInfo);
        }

        public void CheckSavingAccounts(int client_id)
        {
            string queryString = $"SELECT id, amount, rate, ceiling, openingDate FROM SavingAccounts WHERE client_id = '{client_id}';";
            List<string> savingAccountInfo = new List<string> { "id", "amount", "rate", "ceiling", "openingDate" };
            foreach (string item in savingAccountInfo)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, savingAccountInfo);
        }

        public void WithdrawMoney(int ID, double amount)
        {
            string queryAmount = $"SELECT amount FROM CurrentAccounts WHERE client_id={ID};";
            decimal currentAmount = ConnectionDB.ReturnDecimal(queryAmount);
            string queryOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE client_id={ID};";
            decimal overdraft = ConnectionDB.ReturnDecimal(queryOverdraft);

            if (Convert.ToDouble(currentAmount - overdraft) >= amount)
            {
                string queryCurrentAccountID = $"SELECT id FROM CurrentAccounts WHERE client_id={ID};";
                int currentAccountID = ConnectionDB.ReturnID(queryCurrentAccountID);
                DateTime dateOp = DateTime.Now;
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - {amount}) WHERE id = { currentAccountID }; INSERT INTO \"Transaction\" (currentAccount_id, transactionType, amount, \"date\") VALUES({currentAccountID}, 'withdrawal', {amount}, '{dateOp}')";
                ConnectionDB.NonQuerySQL(queryString);
            }
            else
            {
                Console.WriteLine("Not enough money on current account.");
            }
        }

        public static void DoTransfer(double amount, DateTime transferDate)
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
                    Transaction.TransferFromCurrentAccountToSavingAccount(debitClient_id, SavingAccount_id, amount, transferDate);
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
                            Transaction.TransferFromCurrentToCurrentAccount(debitClient_id, clientIdOfExternalAccount, amount, transferDate);
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
                Transaction.TransferFromSavingToCurrentAccount(debitClient_id, SavingAccount_id, amount, transferDate);
            }
            else
            {
                Console.WriteLine("Exiting program due to input error");
            }
        }


        public void ImmediateTransfer(double amount)
        {
            DateTime transferDate = DateTime.Now;
            DoTransfer(amount, transferDate);
        }

        
        
       
    }
}
 