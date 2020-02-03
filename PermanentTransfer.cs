using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class PermanentTransfer : Transaction
    {
        public int NumberIteration { get; set; }
        public DateTimeOffset Frequency { get; set; }
        public DateTime FirstExecution { get; set; }
    }
}