using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class DelayedTransfer : Transaction
    {
        public DateTime DateTimeTransaction { get; set; }
        public DateTimeOffset Delay { get; set; }
    }
}