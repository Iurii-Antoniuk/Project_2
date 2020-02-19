using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person

    { 
        /*public void CheckCurrentAccounts()
        {
            Console.WriteLine("Current Accounts : ");
            int client_id = ID;
            string queryString = $"SELECT id, amount, overdraft, openingDate FROM CurrentAccounts WHERE client_id = '{client_id}';";
            List<string> currentAccountInfo = new List<string> { "id", "amount", "overdraft", "openingDate" };
            foreach (string item in currentAccountInfo)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, currentAccountInfo);
        }*/
        public void CheckSavingAccounts(int debitClient_id)
        {
            Console.WriteLine("Savings Accounts : ");
            int client_id = Person.ID;
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

        public void AddFromBeneficiary(double amount, int id_donator)
        {
            int client_id = ID;
            string queryString1 = $"SELECT amount FROM SavingAccounts WHERE client_id={ client_id};";
            decimal currentAmount = ConnectionDB.ReturnDecimal(queryString1);

            string queryString2 = $"SELECT ceiling FROM SavingAccounts WHERE client_id={ client_id};";
            decimal ceiling = ConnectionDB.ReturnDecimal(queryString2);

            string queryString3 = $"SELECT id FROM SavingAccounts WHERE client_id={ client_id};";
            int savingAccountID = ConnectionDB.ReturnID(queryString3);

            string queryString4 = $"SELECT id FROM Donator WHERE client_id={ client_id} and id={id_donator} ;";
            int id = ConnectionDB.ReturnID(queryString4);

            if (id_donator == id)
            {
                Console.WriteLine("donor ok");
                if ((currentAmount + (decimal)amount) <= ceiling)
                {
                    DateTime dateOp = DateTime.Now;
                    string queryString = $"UPDATE SavingAccounts SET amount = (amount + {amount}) WHERE  client_id = { client_id }; " +
                                         $"INSERT INTO \"Transaction\" (savingAccount_id, transactionType, amount, \"date\") " +
                                         $"VALUES({savingAccountID}, 'Donor_{id}', {amount}, '{dateOp}')";
                    ConnectionDB.NonQuerySQL(queryString);
                }
                else
                {
                    Console.WriteLine("Invalid transaction - exceeding ceilling");
                }
            }
            else
            {
                Console.WriteLine("there is no information about this donor");
            }


            
        }


    }
}
 