using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Timers;

namespace Project_2
{
    public class SavingsAccount : Account
    {
        public decimal Ceiling { get; set; }
        public decimal Interest { get; set; }
        public List<Account> AllowedCreditors { get; set; } = new List<Account>();

        public void CreateSavingAccount(int client_id, double amount)
        {
            Console.WriteLine("Enter the rate (decimal) of the new account : ");
            Interest = Convert.ToDecimal(Console.ReadLine());
            
            Console.WriteLine("Enter the maximum amount you can have on the new account : ");
            Ceiling = Convert.ToDecimal(Console.ReadLine());

            DateTime openingDate = DateTime.Now;

            string queryString = $"INSERT INTO SavingAccounts (client_id, amount, rate, ceiling, openingDate) " +
                                $" VALUES ('{client_id}', '{amount}','{Interest}','{Ceiling}','{openingDate}');";
            ConnectionDB.NonQuerySQL(queryString);
        }

        public void DeleteSavingAccount(int account_id)
        {
            string queryString = $"DELETE FROM SavingAccounts WHERE id ='{account_id}';";
            ConnectionDB.NonQuerySQL(queryString);
        }

        public static void AddInterest(int interval)
        {
            /*DateTime onsetTime = DateTime.Now;
            TimeSpan interval = new TimeSpan(0, 0, 0, 30, 0);

            while (true)
            {
                if (DateTime.Now == onsetTime + interval)
                {
                    ConnectionDB.UpdateSavingAmounts();
                    onsetTime = DateTime.Now;
                }
            }*/

            // Create a timer
            Timer aTimer = new System.Timers.Timer(interval);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += InterestAddition;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void InterestAddition(object source, ElapsedEventArgs e) 
        {
            int numberOFAccounts = CountRows("SavingAccounts");
            if (numberOFAccounts > 0)
            {
                UpdateSavingAmounts();
            }  
        }

        public static void UpdateSavingAmounts()
        {
            int numberOFAccounts = CountRows("SavingAccounts");
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            for (int i = 0; i < numberOFAccounts; i++)
            {
                string queryString = $"UPDATE SavingAccounts SET amount = (amount + amount * rate) WHERE id = 100 + {i};";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public static int CountRows(string tableName)
        {
            string queryString = $"SELECT COUNT(*) FROM {tableName}; ";
            int numberRows = ConnectionDB.ReturnID(queryString);
            return numberRows;
        }
    }
}