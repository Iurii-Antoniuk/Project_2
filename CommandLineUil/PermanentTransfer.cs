using System;
using System.Collections.Generic;
using System.Text;

namespace IKEACmdUtil
{
    public class PermanentTransfer : Transaction
    {
        public void RecordTransferFromCurrentToCurrent(int emitterId, int beneficiaryId, double amount, string firstExecution, string lastExecution, Int32 interval)
        {
            int debitClient_id = Person.ID;
            DateTime firstExecutionDate = CheckDate(firstExecution);
            DateTime lastExecutionDate = CheckDate(lastExecution);

            if (lastExecutionDate <= firstExecutionDate || firstExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid dates");
            }
            else
            {
                QueryTransferFromCurrentToCurrent(emitterId, beneficiaryId, amount, firstExecutionDate, lastExecutionDate, interval);
            }
        }

        public void QueryTransferFromCurrentToCurrent(int emitterId, int beneficiaryId, double amount, DateTime firstExecution, DateTime lastExecution, Int32 interval)
        {
            int debitClient_id = Person.ID;
            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE id = {emitterId}";
            decimal CurrentAccountContent = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE id = {emitterId}";
            decimal CurrentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);

            if (Convert.ToDouble(CurrentAccountContent + CurrentAccountOverdraft) > amount)
            {
                string queryString =
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiaryCurrentAccount_id, amount, executionDate, lastExecutionDate, intervalDays, status) " +
                                 $"VALUES (" +
                                 $"{emitterId}, " +
                                 $"'Money Transfer', " +
                                 $"{beneficiaryId}, " +
                                 $"{amount}, " +
                                 $"'{firstExecution}'," +
                                 $"'{lastExecution}'," +
                                 $"{interval}," +
                                 $"'pending');";
                ConnectionDB.NonQuerySQL(queryString);
            }
        }

        public void RecordTransferFromSavingToCurrent(int emitterId, int beneficiaryId, double amount, string firstExecution, string lastExecution, Int32 interval)
        {
            DateTime firstExecutionDate = CheckDate(firstExecution);
            DateTime lastExecutionDate = CheckDate(lastExecution);

            if (lastExecutionDate <= firstExecutionDate || firstExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid dates");
            }
            else
            {
                QueryTransferFromSavingToCurrent(emitterId, beneficiaryId, amount, firstExecutionDate, lastExecutionDate, interval);
            }
        }

        public void QueryTransferFromSavingToCurrent(int emitterId, int beneficiaryId, double amount, DateTime firstExecution, DateTime lastExecution, Int32 interval)
        {
            string checkSavingAccountContent = $"SELECT amount FROM SavingAccounts WHERE id = {emitterId}";
            decimal SavingAccountContent = ConnectionDB.ReturnDecimal(checkSavingAccountContent);

            if ((Convert.ToDouble(SavingAccountContent) - amount) >= 0)
            {
                string queryString =
                                 $"INSERT INTO \"Transaction\" (savingAccount_id, transactionType, beneficiaryCurrentAccount_id, amount, executionDate, lastExecutionDate, intervalDays, status) " +
                                 $"VALUES (" +
                                 $"{emitterId}, " +
                                 $"'Money Transfer', " +
                                 $"{beneficiaryId}, " +
                                 $"{amount}, " +
                                 $"'{firstExecution}'," +
                                 $"'{lastExecution}'," +
                                 $"{interval}," +
                                 $"'pending');";
                ConnectionDB.NonQuerySQL(queryString);
            }
        }


        public void RecordTransferFromCurrentToSaving(int emitterId, int beneficiaryId, double amount, string firstExecution, string lastExecution, Int32 interval)
        {
            DateTime firstExecutionDate = CheckDate(firstExecution);
            DateTime lastExecutionDate = CheckDate(lastExecution);
            

            if (lastExecutionDate <= firstExecutionDate || firstExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid dates");
            }
            else
            {
                QueryTransferFromCurrentToSaving(emitterId, beneficiaryId, amount, firstExecutionDate, lastExecutionDate, interval);
            }
        }

        public void QueryTransferFromCurrentToSaving(int emitterId, int beneficiaryId, double amount, DateTime firstExecution, DateTime lastExecution, Int32 interval)
        {
            string checkCurrentAccountContent = $"SELECT amount FROM CurrentAccounts WHERE id = {emitterId}";
            decimal CurrentAccountContent = ConnectionDB.ReturnDecimal(checkCurrentAccountContent);
            string getCurrentAccountOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE id = {emitterId}";
            decimal CurrentAccountOverdraft = ConnectionDB.ReturnDecimal(getCurrentAccountOverdraft);

            string checkSavingAccountContent = $"SELECT amount FROM SavingAccounts WHERE id = {beneficiaryId}";
            decimal SavingAccountContent = ConnectionDB.ReturnDecimal(checkSavingAccountContent);
            string checkSavingAccountCeiling = $"SELECT ceiling FROM SavingAccounts WHERE id = {beneficiaryId}";
            decimal SavingAccountCeiling = ConnectionDB.ReturnDecimal(checkSavingAccountCeiling);

            bool IsValidDonator = Client.AddFromBeneficiary(emitterId, beneficiaryId);

            if (Convert.ToDouble(CurrentAccountContent - CurrentAccountOverdraft) >= amount && IsValidDonator)
            {
                if(((Convert.ToDouble(SavingAccountContent)) + amount) < Convert.ToDouble(SavingAccountCeiling) )
                {
                    string queryString =
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiarySavingAccount_id, amount, executionDate, lastExecutionDate, intervalDays, status) " +
                                 $"VALUES (" +
                                 $"{emitterId}, " +
                                 $"'Money Transfer', " +
                                 $"{beneficiaryId}, " +
                                 $"{amount}, " +
                                 $"'{firstExecution}'," +
                                 $"'{lastExecution}'," +
                                 $"{interval}," +
                                 $"'pending');";
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
    }
}