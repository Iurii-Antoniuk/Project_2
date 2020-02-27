using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEAListenerr
{
    class TransactionExecutor
    {
        public Database _database { get; set; }
        public bool TransactionHasToBeExecuted(Transaction transaction)
        { }

        public void ExecuteTransaction(Transaction transaction)
        {
            _database.UpdateTransaction(transaction);
        }
    }
}
