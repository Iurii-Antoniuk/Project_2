using System;
using System.Collections.Generic;

namespace Project_2
{
    class Information
    {
        public static void GetInfoByUserId(int client_id)
        {
            Console.WriteLine("User Information : ");
            string queryString = $"SELECT id, name FROM Person WHERE id = '{client_id}';";
            List<string> clientInfo = new List<string> { "id", "name"};
            foreach (string item in clientInfo)
            {
                Console.Write(item + "\t");
            }
            ConnectionDB.SelectSQL(queryString, clientInfo);
        }

        public static void GetInfoByTransactionDate(DateTime date)
        {
            //Modifier le format DATETIME en DATE dans le script SQL
            
            Console.WriteLine("Transaction Information : ");
            string queryString = $"SELECT id, currentAccount_id, savingAccount_id, transactionType, beneficiaryAccount_id, amount, \"date\" FROM \"Transaction\" WHERE date = '{date}';";
            List<string> transactionInfo = new List<string> { "id", "currentAccount_id", "savingAccount_id",  "transactionType", "beneficiaryAccount_id", "amount", "date" };
            ConnectionDB.SelectSQL(queryString, transactionInfo);
        }

        public static void GetInfoBySavingsAccountId(int savingsAccountId)
        {
            Console.WriteLine("Savings Accounts : ");
            int client_id = Client.ID;
            string queryString = $"SELECT id, amount, rate, ceiling, openingDate FROM SavingAccounts WHERE client_id = '{client_id}' AND id='{savingsAccountId}';";
            List<string> savingAccountInfo = new List<string> { "id", "amount", "rate", "ceiling", "openingDate" };
            foreach (string item in savingAccountInfo)
            {
                Console.Write(item + "\t");
            }
            ConnectionDB.SelectSQL(queryString, savingAccountInfo);
        }

        public static void GetInfoByCurrentAccountId(int currentAccountId)
        {
            Console.WriteLine("Current Accounts : ");
            int client_id = Client.ID;
            string queryString = $"SELECT id, amount, overdraft, openingDate FROM CurrentAccounts WHERE client_id = '{client_id}' AND id='{currentAccountId}';";
            List<string> currentAccountInfo = new List<string> { "id", "amount", "overdraft", "openingDate" };
            foreach (string item in currentAccountInfo)
            {
                Console.Write(item + "\t");
            }
            ConnectionDB.SelectSQL(queryString, currentAccountInfo);
        }
    }
}
