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
            List<string> currentAccountInfo = new List<string> { "id", "amount", "overdraft","openingDate" };
            ConnectionDB.SelectSQL(queryString, currentAccountInfo);
        }


        public void CheckSavingAccounts(int client_id)
        {
            string queryString = $"SELECT id, amount, rate, ceiling, openingDate FROM SavingAccounts WHERE client_id = '{client_id}';";
            List<string> savingAccountInfo = new List<string> { "id", "amount", "rate", "ceiling", "openingDate" };
            ConnectionDB.SelectSQL(queryString, savingAccountInfo);
        }


        public static void WithdrawMoney2(int currentAccountID, double amount)
        {
            string queryString1 = $"SELECT amount FROM CurrentAccounts WHERE id={currentAccountID};";
            decimal currentAmount = ConnectionDB.ReturnDecimal(queryString1);
            string queryString2 = $"SELECT overdraft FROM CurrentAccounts WHERE id={currentAccountID};";
            decimal overdraft = ConnectionDB.ReturnDecimal(queryString2);

            if (Convert.ToDouble(currentAmount - overdraft) >= amount)
            {
                DateTime dateOp = DateTime.Now;
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - {amount}) WHERE id = { currentAccountID }; INSERT INTO \"Transaction\" (currentAccount_id, transactionType, amount, \"date\") VALUES({currentAccountID}, 'withdrawal', {amount}, '{dateOp}')";
                ConnectionDB.NonQuerySQL(queryString);
        }
            else
            {
                Console.WriteLine("You don't have enough money to perform the required transaction");
            }
}
    }
}
 