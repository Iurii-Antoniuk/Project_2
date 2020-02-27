using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEAListenerr
{
    class Transaction
    {
        public int emitterCurrentAccountId { get; set; }
        public int emitterSavingAccountId { get; set; }
        public int beneficiaryCurrentAccountId { get; set; }
        public int beneficiarySavingAccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int intervalDate { get; set; }
        public int id { get; set; }
        public decimal amount { get; set; }

        public Transaction()
        {

            
        }
    }
}
