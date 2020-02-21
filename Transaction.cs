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



        public void QueryTransferFromCurrentToCurrent(int emitterId, int beneficiaryId, double amount, DateTime executionDate)
        {
            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE id = {emitterId}";
            decimal currentAccountContent = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE id = {emitterId}";
            decimal currentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);

            if (Convert.ToDouble(currentAccountContent + currentAccountOverdraft) > amount)
            {
                string queryString =
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiaryCurrentAccount_id, amount, executionDate, status) " +
                                 $"VALUES (" +
                                 $"{emitterId}, " +
                                 $"\'Money Transfer\', " +
                                 $"{beneficiaryId}, " +
                                 $"{amount}, " +
                                 $"\'{executionDate}\'," +
                                 $"\'pending\');";

                ConnectionDB.NonQuerySQL(queryString);
            }
        }

        public void QueryTransferFromSavingToCurrent(int emitterId, int beneficiaryId, double amount, DateTime executionDate)
        {
            string checkSavingAccountContent = $"SELECT amount FROM SavingAccounts WHERE id = {emitterId}";
            decimal savingAccountAmount = ConnectionDB.ReturnDecimal(checkSavingAccountContent);

            if ((Convert.ToDouble(savingAccountAmount) - amount) >= 0)
            {
                string queryString =
                                 $"INSERT INTO \"Transaction\" (savingAccount_id, transactionType, beneficiaryCurrentAccount_id, amount, executionDate, status) " +
                                 $"VALUES (" +
                                 $"{emitterId}, " +
                                 $"\'Money Transfer\', " +
                                 $"{beneficiaryId}, " +
                                 $"{amount}, " +
                                 $"\'{executionDate}\'," +
                                 $"\'pending\');";
                ConnectionDB.NonQuerySQL(queryString);
            }
        }

        public void QueryTransferFromCurrentToSaving(int emitterId, int beneficiaryId, double amount, DateTime firstExecution)
        {
            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE id = {emitterId}";
            decimal currentAccountAmount = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE id = {emitterId}";
            decimal currentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);

            string checkSavingAccountContent = $"SELECT amount FROM SavingAccounts WHERE id = {beneficiaryId}";
            decimal savingAccountAmount = ConnectionDB.ReturnDecimal(checkSavingAccountContent);
            string checkSavingAccountCeiling = $"SELECT ceiling FROM SavingAccounts WHERE id = {beneficiaryId}";
            decimal savingAccountCeiling = ConnectionDB.ReturnDecimal(checkSavingAccountCeiling);

            if (Convert.ToDouble(currentAccountAmount - currentAccountOverdraft) >= amount)
            {
                if (((Convert.ToDouble(savingAccountAmount)) + amount) < Convert.ToDouble(savingAccountCeiling))
                {
                    string queryString =
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiarySavingAccount_id, amount, executionDate, status) " +
                                 $"VALUES (" +
                                 $"{emitterId}, " +
                                 $"\'Money Transfer\', " +
                                 $"{beneficiaryId}, " +
                                 $"{amount}, " +
                                 $"\'{firstExecution}\'," +
                                 $"\'pending\');";
                    Console.WriteLine(queryString);
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
            DateTime dateValue = DateTime.Today;
            try
            {
                dateValue = DateTime.ParseExact(userInput, "d", fr, DateTimeStyles.AllowLeadingWhite);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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