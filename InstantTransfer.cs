using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class InstantTransfer : Transaction
    {
        public void ImmediateTransfer(double amount)
        {
            DateTime transferDate = DateTime.Now;
            Client.DoTransfer(amount, transferDate);
        }
    }
}