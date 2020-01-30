using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class Transaction
    {
        protected Account ActualAccount { get; set; }
        protected DateTime DateTransaction { get; set; }
        protected double Amount { get; set; }
        protected Account DestinationAccount { get; set; }
    }
}