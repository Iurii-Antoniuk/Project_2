using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Client : Person
    { 
        // public string Name { get; set; }  
        // public int Password { get; set; }
        
        private static int _accountNumber = 0;

        public CurrentAccount currentAccount { get; set; }

        public Client (string name, string password, double amount)
        {
            Name = name;
            Password = password;
            int accountNumber = _accountNumber;
            _accountNumber++;
            CurrentAccount currentAccount = new CurrentAccount(accountNumber, amount);
        }

        
    }

   
}
 