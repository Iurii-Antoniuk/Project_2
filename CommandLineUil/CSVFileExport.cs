﻿using System;
using System.Data.SqlClient;
using System.IO;

namespace IKEACmdUtil
{
    class CSVFileExport
    {
        public static void ExportCSVFile(string exportPath)
        {
            string queryString = GetTransactions();
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            Random randomNumber = new Random();
            int a = randomNumber.Next(1, int.MaxValue);

            string exportCsv = $"exportFileDB{a}.csv";

            if (Directory.Exists(exportPath))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    StreamWriter csvFile = new StreamWriter(@exportPath +"\\" + exportCsv);

                    object[] output = new object[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        output[i] = reader.GetName(i);
                    }

                        csvFile.WriteLine(string.Join(", ", output));
                    

                    while (reader.Read())
                    {
                        reader.GetValues(output);
                        csvFile.WriteLine(string.Join(", ", output));
                    }
                    Console.WriteLine("Data export successful.");

                    csvFile.Close();
                    reader.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Data export unsuccessful."+ e);
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("File path does not exist.");
            }
            connection.Close();
        }

        public static string GetTransactions()
        {
            string queryString = $"SELECT id, currentAccount_id, savingAccount_id, transactionType, beneficiaryCurrentAccount_id, beneficiarySavingAccount_id, amount, executionDate, " +
                                $"lastExecutionDate, intervalDays, status FROM \"Transaction\";";
            return queryString;
        }
    }
}
