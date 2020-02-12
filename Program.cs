using System;
using CommandLine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

namespace Project_2
{
    class Program
    {
       class Options

       {
            [Option('l', "login", HelpText = "enter your login")]
            public string EnterLogin { get; set; }

            [Option('v', "verbose", HelpText = "enter your verb")]
            public string EnterVerbose { get; set; }
       }


        [Verb("withdraw", HelpText = "money withdraw")]
        class WithdrawOptions : Options
        {
            [Option('a', "amount", HelpText = "amount to withdraw")]
            public double Amount { get; set; }

            [Option('i', "Account Id", HelpText = "number of your account")]
            public int AccountId { get; set; }

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

            [Option('d', "debit account", Required = true, HelpText="Enter the id of your debited account")]
            public string IdDebitAccount { get; set; }

            [Option('c', "credit account", Required = true, HelpText = "Enter the id of your credited account")]
            public string IdCreditAccount { get; set; }

        }

        [Verb("createC", HelpText = "Client creation")]
        class CreateClientOptions : Options
        {
            [Option('n', "name", Required =true, HelpText = "Client Name")]
            public string ClientName { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount")]
            public double Amount { get; set; }
        }

        [Verb("deleteC", HelpText = "Client deletion")]
        class DeleteClientOptions : Options
        {
            [Option('i', "id", Required =true, HelpText = "id Client to delete")]
            public int IdClient { get; set; }
        }

        [Verb("createSA", HelpText = "Create savings account")]
        class CreateSavingsAccountOptions : Options
        {

            [Option('c', "current account", HelpText = "Create current account")]
            public bool CreateCurrentAccount { get; set; }

            [Option('i', "id", HelpText = "id Client")]
            public int IdClient { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount availble on the account")]
            public double Amount { get; set; }
        }

        [Verb("createCA", HelpText = "Create current account")]
        class CreateCurrentAccountOptions : Options
        {
            [Option('i', "id", HelpText = "id Client")]
            public int IdClient { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount availble on the account")]
            public double Amount { get; set; }
        }

        [Verb("info", HelpText = "Get information")]
        class InfoOptions : Options

        {
            [Option('i', "client id", Required =true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

            [Option('c', "id current account", HelpText = "Enter your current account id")]
            public int IdCurrentAccount { get; set; }
            [Option('s', "id saving account", HelpText = "Enter your saving account id")]
            public int IdSavingAccount { get; set; }
        }

        static void Main(string[] args)
        {

            Parser.Default.ParseArguments<Options, WithdrawOptions, TransferOptions, CreateCurrentAccountOptions, CreateSavingsAccountOptions, InfoOptions, CreateClientOptions
             , DeleteClientOptions>(args)
              //.WithParsed<Options>(RunOptions)
              .WithParsed<WithdrawOptions>(RunWithdrawOptions)
              .WithParsed<TransferOptions>(RunTransferOptions)
              .WithParsed<CreateCurrentAccountOptions>(RunCurrentAccountOptions)
              .WithParsed<CreateSavingsAccountOptions>(RunSavingsAccountOptions)
              .WithParsed<InfoOptions>(RunInfoOptions)
              .WithParsed<CreateClientOptions>(RunCreateClientOptions)
              .WithParsed<DeleteClientOptions>(RunDeleteClientOptions); 

            ConnectionDB.GetConnectionString();

            //Authentification authentification = new Authentification();
            //authentification.Login();
            //authentification.ModifyPassword(3);            

            //Administrator admin = new Administrator();
            //admin.CreateAdmin("admin");
            //Console.WriteLine(Person.Password);
            //admin.CreateSavingAccount(3, 5000);

            //Client client = new Client();
            //client.CheckCurrentAccount(4);
            //client.CheckSavingAccounts(3);

            //Administrator admin = new Administrator();
            //admin.CreateClient("admin", 500);

            //Client.WithdrawMoney(102, 20);
            //Client client = new Client();
            //client.ImmediateTransfer(100);

        }


        static void RunInfoOptions(InfoOptions options)
        {
            Authentification authentification = new Authentification();
            authentification.Login();
            Client client = new Client();
        }

        static void RunWithdrawOptions(WithdrawOptions options)
        {
            Authentification authentification = new Authentification();
            authentification.Login();
            Client client = new Client();
            if (options.AccountId == ConnectionDB.ReturnIdCurrentAccount(options.AccountId))
            {
                Client.WithdrawMoney(options.AccountId, options.Amount);
            }
            else
            {
                Console.WriteLine("Wrong ID account");
            }
        }

        static void RunTransferOptions(TransferOptions options)
        {
            Authentification authentification = new Authentification();
            authentification.Login();

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
        static void RunCreateClientOptions(CreateClientOptions options)
        {
            Authentification authentification = new Authentification();
            if (authentification.Login() == 1)
            { 
                Administrator administrator = new Administrator();
                administrator.CreateClient(options.ClientName, options.Amount);
                Console.WriteLine("Clients password : ");
                Console.WriteLine(Person.Password);
            }
            else
            {
                Console.WriteLine("You can not create a new client");
            }

        }

        static void RunDeleteClientOptions(DeleteClientOptions options)
        {
            Authentification authentification = new Authentification();
            if (authentification.Login() == 1)
            {
                Administrator administrator = new Administrator();
                administrator.DeleteClient(options.IdClient);
            }
            else
            {
                Console.WriteLine("You can not delete a client");
            }
        }

        static void RunCurrentAccountOptions (CreateCurrentAccountOptions options)
        {
            Authentification authentification = new Authentification();
            if (authentification.Login() == 1)
            {
                Administrator administrator = new Administrator();
                administrator.CreateCurrentAccount(options.IdClient, options.Amount);
            }
            else
            {
                Console.WriteLine("You can not create a new current account");
            }
        }
        static void RunSavingsAccountOptions(CreateSavingsAccountOptions options)
        {
            Authentification authentification = new Authentification();
            if (authentification.Login() == 1)
            {
                Administrator administrator = new Administrator();
                administrator.CreateSavingAccount(options.IdClient, options.Amount);
            }
            else
            {
                Console.WriteLine("You can not create a new current account");
            }
        }
    }
}
    

