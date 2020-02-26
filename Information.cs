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
                Console.Write("\t" + item);
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, clientInfo);
        }

        public static void GetInfoByTransactionDate(string date)
        {

            DateTime dateValue = Transaction.CheckDate(date);

            Console.WriteLine("Transaction Information : ");
            string queryString = $"SELECT id, currentAccount_id, savingAccount_id, transactionType, beneficiaryCurrentAccount_id, beneficiarySavingAccount_id, amount, executionDate, " +
                                $"lastExecutionDate, intervalDays, status FROM \"Transaction\" WHERE executionDate = '{dateValue}';";
            List<string> transactionInfo = new List<string> { "id", "currentAccount_id", "savingAccount_id",  "transactionType", "beneficiaryCurrentAccount_id",
                                                        "beneficiarySavingAccount_id", "amount", "executionDate", "lastExecutionDate", "intervalDays", "status" };
            foreach (string item in transactionInfo)
            {
                Console.Write("\t" + item +"," );
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, transactionInfo);
        }

        public static void GetInfoBySavingsAccountId(int savingsAccountId)
        {
            Console.WriteLine("Savings Accounts : ");
            int client_id = GetAccountOwnerId(savingsAccountId, AccountType.Saving);
            string queryString = $"SELECT id, amount, rate, ceiling, openingDate FROM SavingAccounts WHERE client_id = '{client_id}' AND id='{savingsAccountId}';";
            List<string> savingAccountInfo = new List<string> { "id", "amount", "rate", "ceiling", "openingDate" };
            foreach (string item in savingAccountInfo)
            {
                Console.Write("\t" + item);
            }
            Console.WriteLine();

            ConnectionDB.SelectSQL(queryString, savingAccountInfo);
        }

        public static void GetInfoByCurrentAccountId(int currentAccountId)
        {
            Console.WriteLine("Current Accounts : ");
            int ownerId = GetAccountOwnerId(currentAccountId, AccountType.Current);
            string queryString = $"SELECT id, amount, overdraft, openingDate FROM CurrentAccounts WHERE client_id = '{ownerId}' AND id='{currentAccountId}';";
            List<string> currentAccountInfo = new List<string> { "id", "amount", "overdraft", "openingDate" };
            foreach (string item in currentAccountInfo)
            {
                Console.Write("\t" + item);
            }
            Console.WriteLine();

            ConnectionDB.SelectSQL(queryString, currentAccountInfo);
        }

        public static int GetAccountOwnerId(int currentAccountId, AccountType type)
        {
            String tableName;
            if (type == AccountType.Current)
            {
                tableName = "CurrentAccounts";
            }
            else if (type == AccountType.Saving)
            {
                tableName = "SavingAccounts";
            }
            else
            {
                throw new ArgumentException("Account type not valid");
            }

            string queryString = $"SELECT client_id FROM {tableName} WHERE id=" + currentAccountId;
            int accountOwnerId = ConnectionDB.ReturnID(queryString);
            return accountOwnerId;
        }
    }
}
