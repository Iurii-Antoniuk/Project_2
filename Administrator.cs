using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Administrator : Person
        {
        public void CreateAdmin(string name)
        {
            PasswordGenerator();
            string password = CryptPassword(Password);
            string queryString = $"INSERT INTO Person (name, password) VALUES ('{name}', '{password}');";
            ConnectionDB.NonQuerySQL(queryString);
        }

        public void CreateClient(string name, double amount)
        {   
            PasswordGenerator();
            string password = CryptPassword(Password);

            Console.WriteLine("Enter the town of the new client");
            string town = Console.ReadLine();

            Console.WriteLine("Enter the address of the new client");
            string address = Console.ReadLine();

            string queryString = $"INSERT INTO Person (name, password, address, town) VALUES ('{name}', '{password}', '{address}', '{town}' );";
            ConnectionDB.NonQuerySQL(queryString);
            queryString = $"SELECT id FROM Person WHERE name = '{name}' AND password='{password}';";
            int client_id = ConnectionDB.ReturnID(queryString);

            CurrentAccount currentAccount = new CurrentAccount();
            currentAccount.CreateCurrentAccount(client_id, amount);
        }              
        public void DeleteClient(int client_id)
        {
            string queryString = $"DELETE FROM Person WHERE id ='{client_id}';";
            ConnectionDB.NonQuerySQL(queryString);
        }
        
    }
}