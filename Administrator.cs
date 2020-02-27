using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Project_2
{
    public class Administrator : Person
    {
        public void CreateClient(string name, double amount, decimal overdraft)
        {   
            PasswordGenerator();
            string password = CryptPassword(Password);

            string queryString = $"INSERT INTO Person (name, password) VALUES ('{name}', '{password}');";
            ConnectionDB.NonQuerySQL(queryString);
            queryString = $"SELECT id FROM Person WHERE name = '{name}' AND password = '{password}';";
            int client_id = ConnectionDB.ReturnID(queryString);

            CurrentAccount currentAccount = new CurrentAccount();
            currentAccount.CreateCurrentAccount(client_id, amount, overdraft);
        }              
        public void DeleteClient(int client_id, int destinaryAccount)
        {
            EmptySavingAccount(client_id);
            EmptyCurrentAccount(client_id, destinaryAccount);
            string queryString = $"UPDATE Person SET IsActive = 0 WHERE id = {client_id}";
            ConnectionDB.NonQuerySQL(queryString);
        }
        
        public void EmptySavingAccount(int client_id)
        {                       
            List<int> idSavingAccount = ConnectionDB.GetSavingAccountIds(client_id);
            string queryStringGetCurrentAccountId = $"SELECT id FROM CurrentAccounts WHERE client_id = {client_id}";
            int currentAccountId = ConnectionDB.ReturnID(queryStringGetCurrentAccountId);

            foreach (int id in idSavingAccount)
            {
                string queryString = $"SELECT amount FROM SavingAccounts WHERE id = {id};";
                double amount = (double) ConnectionDB.ReturnDecimal(queryString);
                InstantTransfer instant = new InstantTransfer();
                instant.RecordTransferFromSavingToCurrent(id, currentAccountId, amount);
            }
        }

        public void EmptyCurrentAccount(int client_id, int destinaryAccount)
        {
            string queryString = $"SELECT amount FROM CurrentAccounts WHERE client_id = {client_id};";
            double amount = (double)ConnectionDB.ReturnDecimal(queryString);
            string queryStringGetCurrentAccountId = $"SELECT id FROM CurrentAccounts WHERE client_id = {client_id};";
            int currentAccountId = ConnectionDB.ReturnID(queryStringGetCurrentAccountId);
            InstantTransfer instant = new InstantTransfer();
            instant.RecordTransferFromCurrentToCurrent(currentAccountId, destinaryAccount, amount);
        }

    }
}