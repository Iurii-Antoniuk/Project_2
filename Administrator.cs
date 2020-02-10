using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Administrator : Person
        {
        public void CreateClient(string name, double amount)
        {
            PasswordGenerator();
            string password = CryptPassword(Password);
            string queryString = $"INSERT INTO Person (name, password) VALUES ('{name}', '{password}');";
            ConnectionDB.NonQuerySQL(queryString);
            queryString = $"SELECT id FROM Person WHERE name = '{name}' AND password='{password}';";
            int client_id = ConnectionDB.ReturnID(queryString);
            CreateCurrentAccount(client_id, amount);
        }              
        public void DeleteClient(int client_id)
        {
            string queryString = $"DELETE FROM Person WHERE id ='{client_id}');";
            ConnectionDB.NonQuerySQL(queryString);
        }
        public void CreateCurrentAccount (int client_id, double amount)
        {
            Console.WriteLine("Enter the amount of the overdraft of the new account : ");
            decimal overdraft = Convert.ToDecimal(Console.ReadLine());

            DateTime openingDate = DateTime.Now;

            string queryString = $"INSERT INTO CurrentAccounts (client_id, amount, overdraft, openingDate) " +
                                $" VALUES ('{client_id}', '{amount}','{overdraft}','{openingDate}');";
            ConnectionDB.NonQuerySQL(queryString);
        }        
        public void CreateSavingAccount(int client_id, double amount)
        {
            Console.WriteLine("Enter the rate (decimal) of the new account : ");
            decimal rate = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Enter the maximum amount you can have on the new account : ");
            decimal ceiling = Convert.ToDecimal(Console.ReadLine());

            DateTime openingDate = DateTime.Now;

            string queryString = $"INSERT INTO SavingAccounts (client_id, amount, rate, ceiling, openingDate) " +
                                $" VALUES ('{client_id}', '{amount}','{rate}','{ceiling}','{openingDate}');";
            ConnectionDB.NonQuerySQL(queryString);
        }
    }
}