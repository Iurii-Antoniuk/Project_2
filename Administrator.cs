using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Administrator : Person
    {

        public Client Client
        {
            get => default;
            set
            {
            }
        }

        public void CreateAccount()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAccount()
        {
            throw new System.NotImplementedException();
        }
    }
}