using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace Project_2
{
    class CSVFileExport
    {
        public static void ExportCSVFile(string queryString)
        {
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();

            Random randomNumber = new Random();
            int a = randomNumber.Next(1, int.MaxValue);

            string exportPath = "E:\\Project_2\\ExportFile\\";
            string exportCsv = $"exportFileDB{a}.csv";

            StreamWriter csvFile = null;

            if (Directory.Exists(exportPath))
            {
                try
                {

                    SqlCommand command = new SqlCommand(queryString, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    csvFile = new StreamWriter(@exportPath + exportCsv);

                    object[] output = new object[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                        output[i] = reader.GetName(i);
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
                    Console.WriteLine("Data export unsuccessful.");
                    System.Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("File path does not exist.");
            }
            connection.Close();
        }
    }
}
