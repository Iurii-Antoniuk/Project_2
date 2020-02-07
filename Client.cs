using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person
    { 
        /*
         * public Client (string name)
        {
            Name = name;
            
        } */

        /*
        public void CheckAccounts(int id)
        {
            try
            {
                Console.WriteLine("Current account status: ");
                //CheckCurrentAccount(id);
                Console.WriteLine("Saving accounts status: ");
                CheckSavingAccounts(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot display accounts status. " + e.Message);
            }
        }

        */
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

        //public static void WithdrawMoney(int currentAccountID, double amount)
        //{
        //    try
        //    {
        //        DateTime dateOp = DateTime.Now;
        //        string queryString = $"BEGIN TRANSACTION UPDATE CurrentAccounts SET amount = (CASE WHEN((amount - {amount}) >= overdraft) THEN amount - {amount} ELSE overdraft END ) WHERE id = {currentAccountID }; INSERT INTO \"Transaction\" (currentAccount_id, transactionType, amount, \"date\") VALUES({currentAccountID}, 'withdrawal', {amount}, '{dateOp}') COMMIT TRANSACTION; ";
        //        ConnectionDB.NonQuerySQL(queryString);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Looks like you've entered an invalid CurrentAccountID..."+ e.Message);
        //    }
        //}

        public static void WithdrawMoney2(int currentAccountID, double amount)
        {
            string queryString1 = $"SELECT amount FROM CurrentAccounts WHERE id={currentAccountID};";
            decimal currentAmount = ConnectionDB.ReturnAmount(queryString1);
            string queryString2 = $"SELECT overdraft FROM CurrentAccounts WHERE id={currentAccountID};";
            decimal overdraft = ConnectionDB.ReturnOverdraft(queryString2);
            //Console.WriteLine("HUY "+ currentAmount+ "HUY "+overdraft);

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
 