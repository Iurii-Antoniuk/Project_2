using System;
using CommandLine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace Project_2
{
    class Program
    {


        [Verb("login", HelpText = "authentification")]
        class LoginOptions
        {
            [Option('u', "username", Required = true, HelpText = "enter your username")]
            public string Username { get; set; }

            [Option('p', "password", Required = true, HelpText = "enter your password")]
            public string Password { get; set; }
        }

        [Verb("withdraw", HelpText = "money withdraw")]
        class WithdrawOptions
        {
            [Option('a', "amount", HelpText = "amount to withdraw")]
            public double Amount { get; set; }

            [Option('u', "Account Id", HelpText = "number of your account")]
            public Account AccountId { get; set; }

        }

        [Verb("transfer", HelpText = "money transaction")]
        class TransferOptions
        {
            [Option('d', "delayed", HelpText = "You want to do a delayed trasaction")]
            public bool Delayed { get; set; }

            [Option('i', "instant", HelpText = "You want to do a instant trasaction")]
            public bool Instant { get; set; }

            [Option('p', "permanent", HelpText = "You want to do a permanent trasaction")]
            public bool Permanent { get; set; }
        }

        [Verb("Account", HelpText = "Account managment")]
        class AccountOptions
        {
            [Option('c', "create", HelpText = "Create account")]
            public bool CreateAccount { get; set; }

            [Option('d', "delete", HelpText = "Delete Account")]
            public bool DeleteAccount { get; set; }

            [Option('n', "name", HelpText = "Client Name")]
            public string ClientName { get; set; }
        }
        [Verb("Info", HelpText = "Get information")]
        class InfoOptions
        {
            [Option('i', "informations", HelpText = "Get informations on your accounts")]
            public bool GetInfo { get; set; }
        }

        static void Main(string[] args)
        {
           /* Parser.Default.ParseArguments<LoginOptions, WithdrawOptions, TransferOptions, AccountOptions, InfoOptions>(args)
            .WithParsed<LoginOptions>(RunLoginOptions)
            .WithParsed<WithdrawOptions>(RunWithdrawOptions)
            .WithParsed<TransferOptions>(RunTransferOptions)
            .WithParsed<AccountOptions>(RunAccountOptions)
            .WithParsed<InfoOptions>(RunInfoOptions);

            Console.WriteLine("Test de la seconde connexion en-dessous: ");

            ConnectionDB.ConfigConnection();*/


            //Create Table
            Console.WriteLine("Create Table");
            string query = @"CREATE TABLE Products(Id INT PRIMARY KEY IDENTITY(1,1), Name VARCHAR(100) NOT NULL, Price DECIMAL(3,2) NOT NULL)";
            SqlConnection connection = new SqlConnection(@"Data Source = AG\SQLEXPRESS; Initial Catalog = SQL1; Integrated Security = True");
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Table has created created");
            Console.ReadLine();

            // Insert data in the table
            Console.WriteLine("Insert data in the table");
            string query = @"INSERT INTO Products(Name, Price) VALUES ('Tomatoes', 5.47), ('Bananas', 2.78), ('Kiwi', 3.70), ('Eggplant', 7.99)";
            SqlConnection connection = new SqlConnection(@"Data Source = AG\SQLEXPRESS; Initial Catalog = SQL1; Integrated Security = True");
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Table has succefully completed");
            Console.ReadLine();

            // UPDATE data in the table
            Console.WriteLine("UPDATE data in the table");
            string query = @"UPDATE Products SET Name='Grapes', Price=6.01 WHERE Id=3";
            SqlConnection connection = new SqlConnection(@"Data Source = AG\SQLEXPRESS; Initial Catalog = SQL1; Integrated Security = True");
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Table has succefully UPDATED");
            Console.ReadLine();

            // DELETE data in the table
            /*Console.WriteLine("DELETE data in the table");
            string query = @"DELETE FROM Products WHERE Id=4";
            SqlConnection connection = new SqlConnection(@"Data Source = AG\SQLEXPRESS; Initial Catalog = SQL1; Integrated Security = True");
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Data has succefully DELETED");
            Console.ReadLine();*/

            // SELECT data in the table
            Console.WriteLine("SELECT data in the table");
            string query = @"SELECT * FROM Products";
            SqlConnection connection = new SqlConnection(@"Data Source = AG\SQLEXPRESS; Initial Catalog = SQL1; Integrated Security = True");
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader dataread = command.ExecuteReader();
            while (dataread.Read())
            {
                Console.WriteLine(dataread["Id"].ToString() + "     " + dataread["Name"].ToString() + "     " + dataread["Price"].ToString());
            }
            dataread.Close();
            connection.Close();
            Console.WriteLine("SELECTION FINISHED");
            Console.ReadLine();

            // Transaction
            Console.WriteLine("Transaction data in the table");
            SqlConnection connection = new SqlConnection(@"Data Source = AG\SQLEXPRESS; Initial Catalog = SQL1; Integrated Security = True");
            connection.Open();
            SqlCommand command = new SqlCommand();
            SqlTransaction transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.CommandText = @"INSERT INTO Products(Name, Price) VALUES ('Kiwi', 3.70), ('Eggplant', 7.99)";
            command.Transaction = transaction;
            command.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
            Console.WriteLine("Transaction commited");
            Console.ReadLine();*/


        }



        static void RunInfoOptions(InfoOptions options)
        {
            if (options.GetInfo)
            {
                Console.WriteLine("Your current account situation : ");
                // metohde
            }
        }

        static void RunLoginOptions(LoginOptions options)
        {
            Console.WriteLine("Your log");

            //Person.Login(options.Username, options.Password);
        }
        static void RunWithdrawOptions(WithdrawOptions options)
        {
            Transaction.WithdrawMoney(options.AccountId, options.Amount);
        }

        static void RunTransferOptions(TransferOptions options)
        {
            if (options.Delayed)
            {

            }
            if (options.Instant)
            {

            }
            if (options.Permanent)
            {

            }
        }
        static void RunAccountOptions(AccountOptions options)
        {
            if (options.CreateAccount)
            {
                Console.WriteLine("Create Account : ");
                //Administrator.CreateClient(options.ClientName);
            }
            if (options.DeleteAccount)
            {
                Console.WriteLine("Delete Account : ");
                //Administrator.DeleteClient();

            }
        }
    }
}
