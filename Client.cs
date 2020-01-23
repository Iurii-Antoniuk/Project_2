using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person
    {
        public Client(string name, int password)
        {
            _name = name;
            _password = password;
        }

        ~Client()
        {
            System.Console.WriteLine("The client has been erased !");
        }

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
    }

   
}