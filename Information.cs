using System;
using System.Collections.Generic;
using System.Text;

namespace Project_2
{
    class Information
    {
        public void GetInfoByUser(int client_id)
        {
            Console.WriteLine("User Information : ");
            string queryString = $"SELECT id, name FROM Person WHERE client_id = '{client_id}';";
            List<string> clientInfo = new List<string> { "id", "name"};
            foreach (string item in clientInfo)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, clientInfo);
        }

        public static void GetInfoByTransactionDate(DateTime date)
        {
            //problème : error quand il y a des cases NULL
            //Modifier le format DATETIME en DATE dans le script SQL
            
            Console.WriteLine("Transaction Information : ");
            string queryString = $"SELECT id, currentAccount_id, transactionType, amount, \"date\" FROM \"Transaction\" WHERE date = '{date}';";
            List<string> transactionInfo = new List<string> { "id", "currentAccount_id", "transactionType","amount", "date" };
            foreach (string item in transactionInfo)
            {
                Console.Write(item + "\t");
            }
            Console.WriteLine();
            ConnectionDB.SelectSQL(queryString, transactionInfo);
        }
    }
}
