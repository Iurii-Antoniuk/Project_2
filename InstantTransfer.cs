using System;

namespace Project_2
{
    public class InstantTransfer : Transaction
    { 

        public void RecordTransferFromCurrentToCurrent(int creditCurrentAccount_id, double amount)
        {
            int debitClient_id = Person.ID;
            DateTime executionDate = DateTime.Today;

            QueryTransferFromCurrentToCurrent(creditCurrentAccount_id, amount, executionDate);
        }

        

        public void RecordTransferFromSavingToCurrent(int debitSavingAccount_id, double amount)
        {
            int debitClient_id = Person.ID;
            DateTime executionDate = DateTime.Today;
            
            QueryTransferFromSavingToCurrent(debitSavingAccount_id, amount, executionDate);
            
        }

        

        public void RecordTransferFromCurrentToSaving(int SavingAccount_id, double amount)
        {
            int debitClient_id = Person.ID;
            DateTime executionDate = DateTime.Today;
            
            QueryTransferFromCurrentToSaving(SavingAccount_id, amount, executionDate);
        }

        

    }
}