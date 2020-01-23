using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Administrator : Person
    {
        private int _password;
        private string _name;

        public string Name   // property
        {
            get { return _name; }   // get method
            set { _name = value; }  // set method
        }

        public int Password   // property
        {
            get { return _password; }   // get method
            set { _password = value; }  // set method
        }

        public Dictionary<string, int> CreateClients(string name, int password)
        {
            Client client = new Client(name, password);
            clientDict.Add(name, password);
            return clientDict;

        }

        public Dictionary<string, int> DeleteClient(Client client)
        {
            clientDict.Remove(client.Name);
            client = null;
            return clientDict;
        }
    }
}