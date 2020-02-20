using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class DelayedTransfer

    { 
        public void RecordTransferFromCurrentToCurrent(int creditCurrentAccount_id, double amount, string executionDate)
        {
            int debitClient_id = Person.ID;
            DateTime trustableExecutionDate = Transactor.CheckDate(executionDate);

            if (trustableExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid date");
            }
            else
            {
                QueryTransferFromCurrentToCurrent(creditCurrentAccount_id, amount, trustableExecutionDate);
            }
        }

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

        public void RecordTransferFromSavingToCurrent(int debitSavingAccount_id, double amount, string executionDate)
        {
            int debitClient_id = Person.ID;
            DateTime trustableExecutionDate = Transactor.CheckDate(executionDate);


            if (trustableExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid date");
            }
            else
            {
                QueryTransferFromSavingToCurrent(debitSavingAccount_id, amount, trustableExecutionDate);
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

        public void RecordTransferFromCurrentToSaving(int SavingAccount_id, double amount, string executionDate)
        {
            int debitClient_id = Person.ID;
            DateTime trustableExecutionDate = Transactor.CheckDate(executionDate);


            if (trustableExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid date");
            }
            else
            {
                QueryTransferFromCurrentToSaving(SavingAccount_id, amount, trustableExecutionDate);
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
    }
}