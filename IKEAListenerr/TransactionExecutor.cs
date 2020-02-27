using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEAListenerr
{
    class TransactionExecutor
    {
        public Database _database { get; set; }
        
        public bool TransactionHasToBeExecuted(Transaction transaction)
        { 

            return true;
        }

        public static void ExecuteTransaction()
        {
            List<Transaction> transactionList = Database.GetPendingTransactionsInTransaction();

            foreach (Transaction item in transactionList)
            {
                if (item.StartDate == DateTime.Today)
                {
                    if (item.intervalDate != 0)
                    {
                        // Permanent transfer
                        
                        // Type de compte émetteur
                        if (item.emitterCurrentAccountId != 0)
                        {
                            // Type de compte destinataire
                            if(item.beneficiaryCurrentAccountId != 0)
                            {
                                // CToC transfer
                                ExecutePermanentTransferCurrentToCurrent(item.emitterCurrentAccountId, item.beneficiaryCurrentAccountId, item.amount, item.id, item.EndDate, item.intervalDate);
                            }
                            else
                            {
                                // CToS transfer
                                ExecutePermanentTransferCurrentToSaving(item.emitterCurrentAccountId, item.beneficiarySavingAccountId, item.amount, item.id, item.EndDate, item.intervalDate);
                            }
                        }
                        else
                        {
                            if (item.beneficiaryCurrentAccountId != 0)
                            {
                                // SToC transfer
                                ExecutePermanentTransferSavingToCurrent(item.emitterSavingAccountId, item.beneficiaryCurrentAccountId, item.amount, item.id, item.EndDate, item.intervalDate);
                            }
                        }
                        
                    }
                    else
                    {
                        // Transfer instantané
                        // Type de compte émetteur
                        if (item.emitterCurrentAccountId != 0)
                        {
                            // Type de compte destinataire
                            if (item.beneficiaryCurrentAccountId != 0)
                            {
                                // CToC transfer
                                ExecuteInstantTransferCurrentToCurrent(item.emitterCurrentAccountId, item.beneficiaryCurrentAccountId, item.amount, item.id);
                            }
                            else
                            {
                                // CToS transfer
                                ExecuteInstantTransferCurrentToSaving(item.emitterCurrentAccountId, item.beneficiarySavingAccountId, item.amount, item.id);
                            }
                        }
                        else
                        {
                            if (item.beneficiaryCurrentAccountId != 0)
                            {
                                // SToC transfer
                                ExecuteInstantTransferSavingToCurrent(item.emitterSavingAccountId, item.beneficiaryCurrentAccountId, item.amount, item.id);
                            }
                        }

                    }
                }
            }
           // _database.UpdateTransaction(transaction);
        }

        public static void ExecuteInstantTransferCurrentToCurrent(int debitCurrentAccount_id, int beneficiaryCurrentAccount_id, decimal amount, int transaction_id)
        {
            if (CheckOverdraftIsNotOvertaked(debitCurrentAccount_id, amount))
            {
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - @amount) WHERE id = @debitCurrentAccount_id " +
                                        $"UPDATE CurrentAccounts SET amount = (amount + @amount) WHERE id = @beneficiaryCurrentAccount_id " +
                                        $"UPDATE \"Transaction\" SET status = 'done' WHERE id = @transaction_id";
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("amount", amount),
                    new SqlParameter("debitCurrentAccount_id", debitCurrentAccount_id),
                    new SqlParameter("transaction_id", transaction_id),
                    new SqlParameter("beneficiaryCurrentAccount_id", beneficiaryCurrentAccount_id),
                };
                Database.NonQuerySQL(queryString, parameters);
            }
            else
            {
                throw new Exception("Overtaking overdraft. ");
            }
        }

        public static void ExecuteInstantTransferCurrentToSaving(int debitCurrentAccount_id, int beneficiarySavingAccount_id, decimal amount, int transaction_id)
        {
            if (CheckOverdraftIsNotOvertaked(debitCurrentAccount_id, amount) && CheckCeilingIsNotOvertaked(beneficiarySavingAccount_id, amount))
            {
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - @amount) WHERE id = @debitCurrentAccount_id " +
                                     $"UPDATE SavingAccounts SET amount = (amount + @amount) WHERE id = @beneficiarySavingAccount_id " +
                                     $"UPDATE \"Transaction\" SET status = 'done' WHERE id = @transaction_id";
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                new SqlParameter("amount", amount),
                new SqlParameter("debitCurrentAccount_id", debitCurrentAccount_id),
                new SqlParameter("transaction_id", transaction_id),
                new SqlParameter("beneficiarySavingAccount_id", beneficiarySavingAccount_id),
                };
                Database.NonQuerySQL(queryString, parameters);
            }
            else
            {
                throw new Exception("Overtaking overdraft. ");
            }


        }

        public static void ExecuteInstantTransferSavingToCurrent(int debitSavingAccount_id, int beneficiaryCurrentAccount_id, decimal amount, int transaction_id)
        {
            decimal actualSavingAccountAmount = GetAmountOfSavingAccount(debitSavingAccount_id);
            if ((actualSavingAccountAmount - amount) >= 0)
            {
                string queryString = $"UPDATE SavingAccounts SET amount = (amount - @amount) WHERE id = @debitSavingAccount_id " +
                                 $"UPDATE CurrentAccounts SET amount = (amount + @amount) WHERE id = @beneficiaryCurrentAccount_id " +
                                 $"UPDATE \"Transaction\" SET status = 'done' WHERE id = @transaction_id";
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                new SqlParameter("amount", amount),
                new SqlParameter("debitSavingAccount_id", debitSavingAccount_id),
                new SqlParameter("transaction_id", transaction_id),
                new SqlParameter("beneficiaryCurrentAccount_id", beneficiaryCurrentAccount_id),
                };
                Database.NonQuerySQL(queryString, parameters);
            }
            else
            { 
                throw new Exception("Not enough cash stranger ");
            }
        }

        /// Permanent


        public static void ExecutePermanentTransferCurrentToCurrent(int debitCurrentAccount_id, int beneficiaryCurrentAccount_id, decimal amount, int transaction_id, DateTime lastExecutionDate, int intervalDays)
        {
            if(CheckOverdraftIsNotOvertaked(debitCurrentAccount_id, amount))
            {
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - @amount) WHERE id = @debitCurrentAccount_id " +
                                 $"UPDATE CurrentAccounts SET amount = (amount + @amount) WHERE id = @beneficiaryCurrentAccount_id " +
                                 $"UPDATE \"Transaction\" SET status = 'done' WHERE id = @transaction_id" +
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiaryCurrentAccount_id, amount, executionDate, lastExecutionDate, intervalDays, status) " +
                                 $"VALUES " +
                                 $"(@debitCurrentAccount_id, 'Money Transfer', @beneficiaryCurrentAccount_id, @amount, @executionDate, @lastExecutionDate, @intervalDays, 'pending'); ";
                List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("amount", amount),
                new SqlParameter("debitCurrentAccount_id", debitCurrentAccount_id),
                new SqlParameter("transaction_id", transaction_id),
                new SqlParameter("beneficiaryCurrentAccount_id", beneficiaryCurrentAccount_id),
                new SqlParameter("executionDate", DateTime.Today + TimeSpan.FromDays(intervalDays)),
                new SqlParameter("lastExecutionDate", lastExecutionDate),
                new SqlParameter("intervalDays", intervalDays),
            };
                Database.NonQuerySQL(queryString, parameters);
            }
            else
            {
                throw new Exception("Overtaking overdraft.");

            }

        }

        public static void ExecutePermanentTransferCurrentToSaving(int debitCurrentAccount_id, int beneficiarySavingAccount_id, decimal amount, int transaction_id, DateTime lastExecutionDate, int intervalDays)
        {
            if(CheckOverdraftIsNotOvertaked(debitCurrentAccount_id, amount) && CheckCeilingIsNotOvertaked(beneficiarySavingAccount_id, amount))
            {
                string queryString = $"UPDATE CurrentAccounts SET amount = (amount - @amount) WHERE id = @debitCurrentAccount_id " +
                                 $"UPDATE SavingAccounts SET amount = (amount + @amount) WHERE id = @beneficiarySavingAccount_id " +
                                 $"UPDATE \"Transaction\" SET status = 'done' WHERE id = @transaction_id" +
                                 $"INSERT INTO \"Transaction\" (currentAccount_id, transactionType, beneficiarySavingAccount_id, amount, executionDate, lastExecutionDate, intervalDays, status) " +
                                 $"VALUES " +
                                 $"(@debitCurrentAccount_id, 'Money Transfer', @beneficiarySavingAccount_id, @amount, @executionDate, @lastExecutionDate, @intervalDays, 'pending'); ";
                List<SqlParameter> parameters = new List<SqlParameter>
                {
                new SqlParameter("amount", amount),
                new SqlParameter("debitCurrentAccount_id", debitCurrentAccount_id),
                new SqlParameter("transaction_id", transaction_id),
                new SqlParameter("beneficiarySavingAccount_id", beneficiarySavingAccount_id),
                new SqlParameter("executionDate", DateTime.Today + TimeSpan.FromDays(intervalDays)),
                new SqlParameter("lastExecutionDate", lastExecutionDate),
                new SqlParameter("intervalDays", intervalDays),
                };
                Database.NonQuerySQL(queryString, parameters);
            }
            else
            {
                throw new Exception("Unallowed operation");
            }

        }

        public static void ExecutePermanentTransferSavingToCurrent(int debitSavingAccount_id, int beneficiaryCurrentAccount_id, decimal amount, int transaction_id, DateTime lastExecutionDate, int intervalDays)
        {
            if((GetAmountOfSavingAccount(debitSavingAccount_id) - amount ) >=0 )
            {
                string queryString = $"UPDATE SavingAccounts SET amount = (amount - @amount) WHERE id = @debitSavingAccount_id " +
                                 $"UPDATE CurrentAccounts SET amount = (amount + @amount) WHERE id = @beneficiaryCurrentAccount_id " +
                                 $"UPDATE \"Transaction\" SET status = 'done' WHERE id = @transaction_id" +
                                 $"INSERT INTO \"Transaction\" (savingAccount_id, transactionType, beneficiaryCurrentAccount_id, amount, executionDate, lastExecutionDate, intervalDays, status) " +
                                 $"VALUES " +
                                 $"(@debitSavingAccount_id, 'Money Transfer', @beneficiaryCurrentAccount_id, @amount, @executionDate, @lastExecutionDate, @intervalDays, 'pending'); ";
                List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("amount", amount),
                new SqlParameter("debitSaingAccount_id", debitSavingAccount_id),
                new SqlParameter("transaction_id", transaction_id),
                new SqlParameter("beneficiaryCurrentAccount_id", beneficiaryCurrentAccount_id),
                new SqlParameter("executionDate", DateTime.Today + TimeSpan.FromDays(intervalDays)),
                new SqlParameter("lastExecutionDate", lastExecutionDate),
                new SqlParameter("intervalDays", intervalDays),
            };
                Database.NonQuerySQL(queryString, parameters);
            }
            else
            {
                throw new Exception("Unallowed operation");
            }

        }

        public static bool CheckCeilingIsNotOvertaked(int beneficiarySavingAccount_id, decimal amount)
        {
            string queryString = $"SELECT \"ceiling\" FROM SavingAccounts WHERE id = {beneficiarySavingAccount_id};";
            decimal ceiling = Database.ReturnDecimal(queryString);

            string queryAmount = $"SELECT amount FROM SavingAccounts WHERE id = {beneficiarySavingAccount_id};";
            decimal actualAmount = Database.ReturnDecimal(queryAmount);

            if (actualAmount + amount <= ceiling)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CheckOverdraftIsNotOvertaked(int debitCurrentAccount_id, decimal amount)
        {
            string queryOverdraft = $"SELECT overdraft FROM CurrentAccounts WHERE id = {debitCurrentAccount_id};";
            decimal overdraft = Database.ReturnDecimal(queryOverdraft);

            string queryAmount = $"SELECT amount FROM CurrentAccounts WHERE id = {debitCurrentAccount_id};";
            decimal actualAmount = Database.ReturnDecimal(queryAmount);

            if ((actualAmount - amount) >= overdraft)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static decimal GetAmountOfSavingAccount(int debitSavingAccount_id)
        {
            string queryString = $"SELECT amount FROM SavingAccounts WHERE id = {debitSavingAccount_id};";
            decimal savingAccountAmount = Database.ReturnDecimal(queryString);

            return savingAccountAmount;
            
        }
    }
}
