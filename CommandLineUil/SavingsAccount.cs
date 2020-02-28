using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Timers;

namespace IKEACmdUtil
{
    public class SavingsAccount : Account
    {
        public decimal Interest { get; set; } = 0.01M;

        public void CreateSavingAccount(int client_id, decimal amount, decimal ceiling)
        {            
            DateTime openingDate = DateTime.Now;

            if (amount <= ceiling)
            {
                string queryString = $"INSERT INTO SavingAccounts (client_id, amount, rate, ceiling, openingDate) " +
                                     $"VALUES ({client_id}, {amount}, {Interest}, {ceiling},'{openingDate}');";
                ConnectionDB.NonQuerySQL(queryString);

                string queryStringID = $"SELECT id FROM CurrentAccounts WHERE client_id={client_id};";
                int currentAccountId = ConnectionDB.ReturnID(queryStringID);

                string queryStringAddInDonator = $"Declare @ClientId int " +
                                                    $"SELECT @ClientId = client_id FROM Donator WHERE client_id = {client_id} " +
                                                    $"IF(@ClientId IS NULL) " +
                                                    $"BEGIN " +
                                                    $"INSERT INTO Donator(client_id, donatorCA_id) VALUES({client_id}, {currentAccountId}) " +
                                                    $"END ;";
                ConnectionDB.NonQuerySQL(queryStringAddInDonator);
                
            }
            else
            {
                Console.WriteLine("You can' t create a savign account when the amount is bigger than the ceiling");
            }
        }

        public void DeleteSavingAccount(int account_id)
        {
            string queryString = $"DELETE FROM SavingAccounts WHERE id ='{account_id}';";
            ConnectionDB.NonQuerySQL(queryString);
        }

        public static void AddDonators(int donatorCurrentAccountId)
        {
            string queryStringDonator = $"INSERT INTO Donator (client_id, donatorCA_id) VALUES ({Client.ID},{donatorCurrentAccountId});";
            ConnectionDB.NonQuerySQL(queryStringDonator);
        }
        
    }
}