using System;
using System.Globalization;

namespace IKEACmdUtil
{
    public abstract class Transaction
    {

        public void QueryTransferFromCurrentToCurrent(int emitterId, int beneficiaryId, double amount, DateTime executionDate)
        {
            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE id = {emitterId}";
            decimal currentAccountContent = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE id = {emitterId}";
            decimal currentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);

            if (Convert.ToDouble(currentAccountContent - currentAccountOverdraft) > amount)
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

            
            bool IsValidDonator = Client.AddFromBeneficiary(emitterId, beneficiaryId);

            if (Convert.ToDouble(currentAccountAmount - currentAccountOverdraft) >= amount && IsValidDonator==true)
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
            if ((!DateTime.TryParseExact(userInput, "d", fr, DateTimeStyles.AllowLeadingWhite, out dateValue)))
            {
                throw new ArgumentException("Invalid Date");
            }
            return dateValue;
        }
    }
}