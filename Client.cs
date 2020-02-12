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
    }
}
 