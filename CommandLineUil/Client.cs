using System;
using System.Collections.Generic;

namespace IKEACmdUtil
{
    public class Client : Person
    {
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
                                     $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, amount, executionDate, \"status\") " +
                                     $"VALUES({currentAccountID}, 'withdraw', {amount}, '{dateOp}', \'done\')";
                ConnectionDB.NonQuerySQL(queryString);
            }
            else
            {
                Console.WriteLine("Not enough money on current account.");
            }
        }

        public static bool AddFromBeneficiary(int emitterId, int beneficiaryId)
        {
            string queryString1 = $"SELECT client_id FROM SavingAccounts WHERE id={beneficiaryId};";
            int client_id = ConnectionDB.ReturnID(queryString1);
            string queryString = $"SELECT id FROM Donator WHERE client_id={client_id} and donatorCA_id={emitterId} ;";

            try
            {
                int id = ConnectionDB.ReturnID(queryString);
                return true;
            }
            catch
            {
                throw new ArgumentException("Selected Account is invalid");
            }      
        }
    }
}
 