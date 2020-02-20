using System;
using System.Globalization;

namespace Project_2
{
    public abstract class Transaction
    {
        protected Account ActualAccount { get; set; }
        protected DateTime DateTransaction { get; set; }
        protected double Amount { get; set; }
        protected Account DestinationAccount { get; set; }



        public void QueryTransferFromCurrentToCurrent(int creditCurrentAccount_id, double amount, DateTime executionDate)
        {
            int debitClient_id = Person.ID;
            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE client_id = {debitClient_id}";
            decimal CurrentAccountContent = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE client_id = {debitClient_id}";
            decimal CurrentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);

            if (Convert.ToDouble(CurrentAccountContent + CurrentAccountOverdraft) > amount)
            {
                string queryString =
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiaryCurrentAccount_id, amount, executionDate, status) " +
                                 $"VALUES (" +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"\'Money Transfer\', " +
                                 $"{creditCurrentAccount_id}, " +
                                 $"{amount}, " +
                                 $"\'{executionDate}\'," +
                                 $"\'pending\');";
                ConnectionDB.NonQuerySQL(queryString);
            }
        }

        public void QueryTransferFromSavingToCurrent(int debitSavingAccount_id, double amount, DateTime executionDate)
        {
            int debitClient_id = Person.ID;
            string checkSavingAccountContent = $"SELECT amount FROM SavingAccounts WHERE id = {debitSavingAccount_id}";
            decimal SavingAccountContent = ConnectionDB.ReturnDecimal(checkSavingAccountContent);

            if ((Convert.ToDouble(SavingAccountContent) - amount) >= 0)
            {
                string queryString =
                                 $"INSERT INTO \"Transaction\" (savingAccount_id, transactionType, beneficiaryCurrentAccount_id, amount, executionDate, status) " +
                                 $"VALUES (" +
                                 $"{debitSavingAccount_id}, " +
                                 $"\'Money Transfer\', " +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"{amount}, " +
                                 $"\'{executionDate}\'," +
                                 $"\'pending\');";
                ConnectionDB.NonQuerySQL(queryString);
            }
        }

        public void QueryTransferFromCurrentToSaving(int SavingAccount_id, double amount, DateTime firstExecution)
        {
            int debitClient_id = Person.ID;
            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE client_id = {debitClient_id}";
            decimal CurrentAccountContent = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE client_id = {debitClient_id}";
            decimal CurrentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);

            string checkSavingAccountContent = $"SELECT amount FROM SavingAccounts WHERE id = {SavingAccount_id}";
            decimal SavingAccountContent = ConnectionDB.ReturnDecimal(checkSavingAccountContent);
            string checkSavingAccountCeiling = $"SELECT ceiling FROM SavingAccounts WHERE id = {SavingAccount_id}";
            decimal SavingAccountCeiling = ConnectionDB.ReturnDecimal(checkSavingAccountCeiling);

            if (Convert.ToDouble(CurrentAccountContent - CurrentAccountOverdraft) >= amount)
            {
                if (((Convert.ToDouble(SavingAccountContent)) + amount) > Convert.ToDouble(SavingAccountCeiling))
                {
                    string queryString =
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiaryAccount_id, amount, executionDate, status) " +
                                 $"VALUES (" +
                                 $"(SELECT id FROM CurrentAccounts WHERE client_id = {debitClient_id}), " +
                                 $"\'Money Transfer\', " +
                                 $"(SELECT id FROM SavingAccounts WHERE client_id = {debitClient_id}), " +
                                 $"{amount}, " +
                                 $"\'{firstExecution}\'," +
                                 $"\'pending\');";
                    ConnectionDB.NonQuerySQL(queryString);
                }
                else
                {
                    Console.WriteLine($"Impossible transfer. Transfer would exceed ceiling limit.");
                }
            }
            else
            {
                Console.WriteLine($"There is not enough money on current account to perform transfer");
            }
        }

        public static DateTime CheckDate(string userInput)
        {
            CultureInfo fr = new CultureInfo("fr");
            DateTime dateValue;
            if (!DateTime.TryParseExact(userInput, "d", fr, DateTimeStyles.AllowLeadingWhite, out dateValue))
            {
                throw new ArgumentException("Unvalid date format. Accepted format dd/mm/yyyy.");
            }
            else
            {
                dateValue = Convert.ToDateTime(userInput);
            }

            return dateValue;
        }


        public int GetClientIDFromAccountId(int account_id)
        {
            string queryStringIdFromOtherClient = $"SELECT client_id FROM CurrentAccounts WHERE id = {account_id}";
            int clientIdOfExternalAccount = ConnectionDB.ReturnID(queryStringIdFromOtherClient);

            if (clientIdOfExternalAccount > 0)
            {
                return clientIdOfExternalAccount;
            }
            else
            {
                return 0;
            }
        }
    }
}