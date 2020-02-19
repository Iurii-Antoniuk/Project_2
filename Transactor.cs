using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Project_2
{
    public class Transactor
    {
        public static int GetSavingAccountIdFromClientChoice(int debitClient_id)
        {
            Client client = new Client();
            client.CheckSavingAccounts(debitClient_id);
            Console.WriteLine("Specify the saving account id for the money transfer. ");
            int SavingAccount_id = Convert.ToInt32(Console.ReadLine());

            return SavingAccount_id;
        }

        public static DateTime GetCheckedDate()
        {
            Console.WriteLine("Enter a date (accepted format DD/MM/YYYY): ");
            string input = Console.ReadLine();
            CultureInfo fr = new CultureInfo("fr");
            DateTime dateValue;
            while (!DateTime.TryParseExact(input, "d", fr, DateTimeStyles.AllowLeadingWhite, out dateValue))
            {
                Console.WriteLine("The format of the time is not right");
                Console.WriteLine("Give good format (accepted format DD/MM/YYYY) of date :");
                input = Console.ReadLine();
            }
            Console.WriteLine("Entered date: " + dateValue.ToString("d"));

            return dateValue;
        }

        public static void TransferFromCurrentToCurrentAccount(int debitClient_id, int creditClient_id, double amount)
        {
            DateTime date = DateTime.Today;

            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE client_id = {debitClient_id}";
            decimal CurrentAccountContent = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE client_id = {debitClient_id}";
            decimal CurrentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);


            if (Convert.ToDouble(CurrentAccountContent - CurrentAccountOverdraft) >= amount)
            {
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - {amount}) WHERE client_id = {debitClient_id} " +
                                 $"UPDATE CurrentAccounts SET amount = (amount + {amount}) WHERE client_id = {creditClient_id} " +
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiaryAccount_id, amount, \"date\") " +
                                 $"VALUES(" +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"\'Money Transfer\', " +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {creditClient_id}), " +
                                 $"{amount}, " +
                                 $"\'{date}\');";
            ConnectionDB.NonQuerySQL(queryString);
            }
            else
            {
                Console.WriteLine($"There is not enough money on current account to perform transfer");
            }
        }

        public static void TransferFromSavingToCurrentAccount(int debitClient_id, int savingAccount_id, double amount)
        {
            DateTime date = DateTime.Today;

            string checkSavingAccountContent = $"SELECT amount FROM SavingAccounts WHERE client_id = {debitClient_id}";
            decimal savingAccountContent = ConnectionDB.ReturnDecimal(checkSavingAccountContent);

            if (Convert.ToDouble(savingAccountContent) >= amount)
            {
                string queryString = $"UPDATE SavingAccounts SET amount = (amount - {amount}) WHERE id = {savingAccount_id} " +
                                 $"UPDATE CurrentAccounts SET amount = (amount + {amount}) WHERE client_id = {debitClient_id}; " +
                                 $"INSERT INTO \"Transaction\" (SavingAccount_id, transactionType, beneficiaryAccount_id, amount, \"date\") " +
                                 $"VALUES(" +
                                 $"(SELECT id FROM SavingAccounts WHERE id = {savingAccount_id}), " +
                                 $"\'Money Transfer\', " +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"{amount}, " +
                                 $"\'{date}\');";
            ConnectionDB.NonQuerySQL(queryString);
            }
            else
            {
                Console.WriteLine($"There is not enough money on saving account {savingAccount_id} to perform transfer");
            }
        }

        public static void TransferFromCurrentAccountToSavingAccount(int debitClient_id, int savingAccount_id, double amount)
        {
            DateTime date = DateTime.Today;

            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE client_id = {debitClient_id}";
            decimal CurrentAccountContent = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE client_id = {debitClient_id}";
            decimal CurrentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);


            if (Convert.ToDouble(CurrentAccountContent - CurrentAccountOverdraft) >= amount)
            {
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - {amount}) WHERE client_id = {debitClient_id} " +
                                 $"UPDATE SavingAccounts SET amount = (amount + {amount}) WHERE id = {savingAccount_id} " +
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, SavingAccount_id, amount, \"date\") " +
                                 $"VALUES(" +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"\'Money Transfer\', " +
                                 $"(SELECT id FROM SavingAccounts WHERE id = {savingAccount_id}), " +
                                 $"{amount}, " +
                                 $"\'{date}\');";
                ConnectionDB.NonQuerySQL(queryString);
            }
            else
            {
                Console.WriteLine($"There is not enough money on current account to perform transfer");
            }
            
        }
    }
}