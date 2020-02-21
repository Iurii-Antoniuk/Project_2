using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    public class DelayedTransfer : Transaction

    { 
        public void RecordTransferFromCurrentToCurrent(int creditCurrentAccount_id, int beneficiaryId, double amount, string executionDate)
        {
            int debitClient_id = Person.ID;
            DateTime trustableExecutionDate = CheckDate(executionDate);
            
            if (trustableExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid date");
            }
            else
            {
                QueryTransferFromCurrentToCurrent(creditCurrentAccount_id, beneficiaryId, amount, trustableExecutionDate);
            }
        }

        
        public void RecordTransferFromSavingToCurrent(int debitSavingAccount_id, int beneficiaryId, double amount, string executionDate)
        {
            int debitClient_id = Person.ID;
            DateTime trustableExecutionDate = CheckDate(executionDate);


            if (trustableExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid date");
            }
            else
            {
                QueryTransferFromSavingToCurrent(debitSavingAccount_id, beneficiaryId, amount, trustableExecutionDate);
            }
        }

        

        public void RecordTransferFromCurrentToSaving(int SavingAccount_id, int beneficiaryId, double amount, string executionDate)
        {
            int debitClient_id = Person.ID;
            DateTime trustableExecutionDate = CheckDate(executionDate);


            if (trustableExecutionDate < DateTime.Today)
            {
                throw new ArgumentException("Unvalid date");
            }
            else
            {
                QueryTransferFromCurrentToSaving(SavingAccount_id, beneficiaryId, amount, trustableExecutionDate);
            }
        }

    }
}