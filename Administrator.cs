using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Administrator : Person
    {
        
        
        public string CreateClient(string name, double amount)
        {
            string password = PasswordGenerator();
            Client client = new Client(name, password);
            Console.WriteLine(name + " has been created.");

            CreateCurrentAccount(client, amount);

            string queryString = $"INSERT INTO Person (name, password) VALUES ('{name}', '{password}');";
            ConnectionDB.NonQuerySQL(queryString);

            queryString = $"SELECT * FROM Person WHERE name = '{name}';";
            List<string> listeTest = new List<string> { "id", "name", "password" };
            ConnectionDB.SelectSQL(queryString, listeTest);

            return password;
        }

       

        public void DeleteClient(Client client)
        {
            client = null;
            Console.WriteLine(Name + " has been erased.");

            //trouver le bon client à effacer dans la BdD et effacer tous ces comptes à condition qu'il soit = 0
        }

        public void CreateCurrentAccount (Client client, double amount)
        {
            string idAccount = IdGenerator();

            CurrentAccount currentAccount = new CurrentAccount(idAccount, amount);
            Console.WriteLine("Account number" + idAccount + "has been created");
        }
        
        public void CreateSavingAccount(Client client, double amount)
        {
            string idAccount = IdGenerator();
            SavingsAccount savingsAccount = new SavingsAccount(idAccount, amount);
            Console.WriteLine("Account number" + idAccount + "has been created");                       
        }

    }
}