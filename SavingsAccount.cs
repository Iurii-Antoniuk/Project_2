using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Timers;

namespace Project_2
{
    public class SavingsAccount : Account
    {
        public double Ceiling { get; set; } = 10000;
        public double Interest { get; set; } = 0.01;
        public List<Account> AllowedCreditors { get; set; } = new List<Account>();

        public SavingsAccount(string accountNumber, double amount)
        {

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
            aTimer.Elapsed += interestAddition;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void interestAddition(object source, ElapsedEventArgs e) 
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