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
            [Option('l', "login", Required = true, HelpText = "enter your login")]
            public string EnterLogin { get; set; }

            [Option('v', "verbose", Required = true, HelpText = "enter your verb")]
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
            [Option('n', "name", HelpText = "Client Name")]
            public string ClientName { get; set; }

            [Option('a', "amount", HelpText = "amount")]
            public double Amount { get; set; }
        }

        [Verb("deleteC", HelpText = "Client deletion")]
        class DeleteClientOptions : Options
        {
            [Option('n', "name", HelpText = "Client Name")]
            public string ClientName { get; set; }

            [Option('a', "amount", HelpText = "amount to withdraw")]
            public double Amount { get; set; }

            [Option('i', "id", Required =true, HelpText = "id Client to delete")]
            public int IdClient { get; set; }
        }

        [Verb("Account", HelpText = "Account managment")]
        class AccountOptions : Options
        {
            [Option('c', "create", HelpText = "Create account")]
            public bool CreateAccount { get; set; }

            [Option('d', "delete", HelpText = "Delete Account")]
            public bool DeleteAccount { get; set; }

            [Option('i', "id", HelpText = "id Client")]
            public string IdClient { get; set; }
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

            Parser.Default.ParseArguments<Options, WithdrawOptions, TransferOptions, AccountOptions, InfoOptions, CreateClientOptions
             , DeleteClientOptions>(args)
              //.WithParsed<Options>(RunOptions)
              .WithParsed<WithdrawOptions>(RunWithdrawOptions)
              .WithParsed<TransferOptions>(RunTransferOptions)
              .WithParsed<AccountOptions>(RunAccountOptions)
              .WithParsed<InfoOptions>(RunInfoOptions)
              .WithParsed<CreateClientOptions>(RunCreateClientOptions)
              .WithParsed<DeleteClientOptions>(RunDeleteClientOptions);

            Console.WriteLine("Welcome on bank application");


            //Authentification authentification = new Authentification();
            //authentification.Login();
            //authentification.ModifyPassword(3);


            //ConnectionDB.GetConnectionString();

            //Administrator admin = new Administrator();
            //admin.CreateClient("Amelia", 6000);
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
                //Administrator.CreateAccount(options.ClientName);
            }
            if (options.DeleteAccount)
            {
                Console.WriteLine("Delete Account : ");
                //Administrator.DeleteAccounts();

            }
        }


        static void RunCreateClientOptions(CreateClientOptions options)
        {
            Authentification authentification = new Authentification();
            authentification.Login();
            Administrator administrator = new Administrator();
            administrator.CreateClient(options.ClientName, options.Amount);

        }

        static void RunDeleteClientOptions(DeleteClientOptions options)
        {
            Authentification authentification = new Authentification();
            authentification.Login();
            Administrator administrator = new Administrator();
            administrator.DeleteClient(options.IdClient);
        }
    }
}
    

