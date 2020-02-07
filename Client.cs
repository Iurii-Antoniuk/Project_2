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
                //CheckCurrentAccount(id);
                Console.WriteLine("Saving accounts status: ");
                CheckSavingAccounts(id);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot display accounts status. " + e.Message);
            }
        }

        //public static void CheckCurrentAccount(int id)
        //{
        //    string queryString = $"SELECT * FROM CurrentAccounts WHERE id = {id};";
        //    ConnectionDB.SelectSQL
        //}

        public static void CheckSavingAccounts(int id)
        {

        }

        public static void WithdrawMoney(int currentAccountID, double amount)
        {
            try
            {
                DateTime dateOp = DateTime.Now;
                string queryString = $"BEGIN TRANSACTION UPDATE CurrentAccounts SET amount = (CASE WHEN((amount - {amount}) >= overdraft) THEN amount - {amount} ELSE overdraft END ) WHERE id = {currentAccountID }; INSERT INTO \"Transaction\" (currentAccount_id, transactionType, amount, \"date\") VALUES({currentAccountID}, 'withdrawal', {amount}, '{dateOp}') COMMIT TRANSACTION; ";
                ConnectionDB.NonQuerySQL(queryString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Looks like you've entered an invalid CurrentAccountID..."+ e.Message);
            }
        }

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
 