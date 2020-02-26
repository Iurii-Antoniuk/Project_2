using System;

namespace Project_2
{
    public class InstantTransfer : Transaction
    { 

        public void RecordTransferFromCurrentToCurrent(int emitterId, int beneficiaryId, double amount)
        {
            int debitClient_id = Person.ID;
            DateTime executionDate = DateTime.Today;

            QueryTransferFromCurrentToCurrent(emitterId, beneficiaryId, amount, executionDate);
        }

        public void RecordTransferFromSavingToCurrent(int debitSavingAccount_id, int beneficiaryId, double amount)
        {
            int debitClient_id = Person.ID;
            DateTime executionDate = DateTime.Today;
            
            QueryTransferFromSavingToCurrent(debitSavingAccount_id, beneficiaryId, amount, executionDate);
        }

        public void RecordTransferFromCurrentToSaving(int SavingAccount_id, int beneficiaryId, double amount)
        {
            int debitClient_id = Person.ID;
            DateTime executionDate = DateTime.Today;
            
            QueryTransferFromCurrentToSaving(SavingAccount_id, beneficiaryId, amount, executionDate);
        }
    }
}