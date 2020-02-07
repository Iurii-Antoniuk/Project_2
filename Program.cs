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
        class Options
        {
            [Option('l', "login", Required = true, HelpText = "enter your login")]
            public string EnterLogin { get; set; }

            [Option('v', "verbose", Required = true, HelpText = "enter your verbose")]
            public string Password { get; set; }
        }

        [Verb("withdraw", HelpText = "money withdraw")]
        class WithdrawOptions : Options
        {
            [Option('a', "amount", HelpText = "amount to withdraw")]
            public double Amount { get; set; }

            [Option('u', "Account Id", HelpText = "number of your account")]
            public Account AccountId { get; set; }

        }

        [Verb("transfer", HelpText = "money transaction")]
        class TransferOptions : Options
        {
            [Option('d', "delayed", HelpText = "You want to do a delayed trasaction")]
            public bool Delayed { get; set; }

            [Option('i', "instant", HelpText = "You want to do a instant trasaction")]
            public bool Instant { get; set; }

            [Option('p', "permanent", HelpText = "You want to do a permanent trasaction")]
            public bool Permanent { get; set; }
        }

        [Verb("Account", HelpText = "Account managment")]
        class AccountOptions : Options
        {
            [Option('c', "create", HelpText = "Create account")]
            public bool CreateAccount { get; set; }

            [Option('d', "delete", HelpText = "Delete Account")]
            public bool DeleteAccount { get; set; }

            [Option('n', "name", HelpText = "Client Name")]
            public string ClientName { get; set; }
        }

        [Verb("Client", HelpText = "Client managment")]
        class ClientOptions : Options
        {
            [Option('c', "create", HelpText = "Create client")]
            public bool CreateClient { get; set; }

            [Option('d', "delete", HelpText = "Delete client")]
            public bool DeleteClient { get; set; }

            [Option('n', "name", HelpText = "Client Name")]
            public string ClientName { get; set; }
        }
        [Verb("info", HelpText = "Get information")]
        class InfoOptions
        {
            /* [Option('i', "informations", HelpText = "Get informations on your accounts")]
            public bool GetInfo { get; set; } */

            [Option('i', "client id", HelpText = "Enter your client id")]
            public int IdClient { get; set; }

            [Option('c', "id current account", HelpText ="Enter your current account id")]
            public int IdCurrentAccount { get; set; }
            [Option('s', "id saving account", HelpText = "Enter your saving account id")]
            public int IdSavingAccount { get; set; }
        }

        static void Main(string[] args)
        {
            /*Parser.Default.ParseArguments<Options, WithdrawOptions, TransferOptions, AccountOptions, ClientOptions, InfoOptions>(args)
             .WithParsed<Options>(RunOptions)
             .WithParsed<WithdrawOptions>(RunWithdrawOptions)
             .WithParsed<TransferOptions>(RunTransferOptions)
             .WithParsed<AccountOptions>(RunAccountOptions)
             .WithParsed<ClientOptions>(RunClientOptions)
             .WithParsed<InfoOptions>(RunInfoOptions); */

             Console.WriteLine("Test de la seconde connexion");
             ConnectionDB.GetConnectionString();

            //Administrator admin = new Administrator();
            //admin.CreateClient("Claire", 3000);
            //admin.CreateSavingAccount(3, 1000);

            //Client client = new Client();
            //client.CheckCurrentAccount(4);
            //client.CheckSavingAccounts(3);

            Console.WriteLine("Enter your password  : ");
            string password = Console.ReadLine();
            Client client = new Client();
            if (password == ConnectionDB.ReturnPassword(3))
            {
                Console.WriteLine("You did it !");
                /*if (100 == ConnectionDB.ReturnIdCurrentAccount(3))
                {

                    Console.WriteLine("Information about your current account : ");
                    client.CheckCurrentAccount(100);
                }
                /*if (100 == ConnectionDB.ReturnIdSavingAccount(3))
                {
                    Console.WriteLine("Information about your savings account : ");
                    client.CheckSavingAccounts(100);
                }
                else
                {
                    Console.WriteLine("You have entered a wrong account number");
                }*/
            }
            else
            {
                Console.WriteLine("You have entered a wrong password");
            }

        }



        static void RunInfoOptions(InfoOptions options)
        {
            Console.WriteLine("Enter your password  : ");
            string password = Console.ReadLine();
            Client client = new Client();
            if (password == ConnectionDB.ReturnPassword(options.IdClient))
            {
                if (options.IdCurrentAccount == ConnectionDB.ReturnIdCurrentAccount(options.IdClient))
                {
                    
                    Console.WriteLine("Information about your current account : ");
                    client.CheckCurrentAccount(options.IdCurrentAccount);
                }
                if (options.IdSavingAccount == ConnectionDB.ReturnIdSavingAccount(options.IdClient))
                {
                    Console.WriteLine("Information about your savings account : ");
                    client.CheckSavingAccounts(options.IdSavingAccount);
                }
                else
                {
                    Console.WriteLine("You have entered a wrong account number");
                }
            }
            else
            {
                Console.WriteLine("You have entered a wrong password");
            }
            
        }

        static void RunOptions(Options options)
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

        static void RunClientOptions(ClientOptions options)
        {

        }
    }
}
