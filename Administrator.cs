using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Administrator : Person
    {
        public void CreateClients(string name, int password)
        {
            Client client = new Client(name, password);
            Console.WriteLine(client.Name + " has been created.");
            CurrentAccount currentAccount = new CurrentAccount(client);
        }

        public void DeleteClient(Client client)
        {
            client = null;
            Console.WriteLine(client.Name + " has been erased.");
        }

        public void CreateCurrentAccount(Client client, double overdraft)
        {
            if (client.CurrentAccount)
            {

            }
        }
    }
}