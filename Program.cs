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

            [Option('a', "amount", Required = true, HelpText = "amount to transfer")]
            public double Amount { get; set; }

            [Option('t', "date time", HelpText="Date where the transfer will happen")]
            public DateTime Date { get; set; }

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

            [Option('i', "id", HelpText = "id Client")]
            public int IdClient { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount availble on the account")]
            public double Amount { get; set; }
        }

        [Verb("deleteSA", HelpText = "Delete savings account")]
        class DeleteSavingsAccountOptions : Options
        {
            [Option('i', "id", HelpText = "id Savings Account")]
            public int IdSavingsAccount{ get; set; }

        }

        [Verb("infoAccounts", HelpText = "Get information about your accounts")]
        class InfoAccountsOptions : Options

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

            /*Parser.Default.ParseArguments<Options, WithdrawOptions, TransferOptions, CreateSavingsAccountOptions, DeleteSavingsAccountOptions, InfoAccountsOptions, CreateClientOptions
             , DeleteClientOptions>(args)
              //.WithParsed<Options>(RunOptions)
              .WithParsed<WithdrawOptions>(RunWithdrawOptions)
              .WithParsed<TransferOptions>(RunTransferOptions)
              .WithParsed<CreateSavingsAccountOptions>(RunCreateSavingsAccountOptions)
              .WithParsed<DeleteSavingsAccountOptions>(RunDeleteSavingsAccountOptions)
              .WithParsed<InfoAccountsOptions>(RunAccountsInfoOptions)
              .WithParsed<CreateClientOptions>(RunCreateClientOptions)
              .WithParsed<DeleteClientOptions>(RunDeleteClientOptions); */

            ConnectionDB.GetConnectionString();

            DateTime date = new DateTime(2020, 2, 12);
            Information.GetInfoByTransactionDate(date);


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

            //ConnectionDB.GetConnectionString();

            //Administrator admin = new Administrator();
            //admin.CreateClient("Gontrand", 6000);
            //Console.WriteLine(Person.Password);
            //admin.CreateSavingAccount(3, 5000);

            //Administrator admin = new Administrator();
            //admin.CreateClient("admin", 500);

            //Client.WithdrawMoney(102, 20);
            //Client client = new Client();
            //client.ImmediateTransfer(100);

        }

        static void RunAccountsInfoOptions(InfoAccountsOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();
            if (id == options.IdClient)
            {
                Client client = new Client();
                client.CheckCurrentAccounts();
                client.CheckSavingAccounts();
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
            
        }

        static void RunWithdrawOptions(WithdrawOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();

            if ( id == options.AccountId && id!=1)
            {
                Client client = new Client();
                client.WithdrawMoney(options.Amount);
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
        }

        static void RunTransferOptions(TransferOptions options)
        {
            Authentification authentification = new Authentification();
            authentification.Login();

            //mettre transactions dans la classe transaction et créer une instance de transaction
            
            if (options.Delayed)
            {
                Client.ClientDelayedTransfer(options.Date,options.Amount);
            }
            if (options.Instant)
            {
                Client client = new Client();
                client.ImmediateTransfer(options.Amount);
            }
            if (options.Permanent)
            {
                //Client client = new Client();

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

        static void RunCreateSavingsAccountOptions(CreateSavingsAccountOptions options)
        {
            Authentification authentification = new Authentification();
            if (authentification.Login() == 1)
            {
                SavingsAccount savingsAccount = new SavingsAccount();
                savingsAccount.CreateSavingAccount(options.IdClient, options.Amount);
            }
            else
            {
                Console.WriteLine("You can not create a new current account");
            }
        }

        static void RunDeleteSavingsAccountOptions(DeleteSavingsAccountOptions options)
        {
            Authentification authentification = new Authentification();
            if (authentification.Login() == 1)
            {
                SavingsAccount savingsAccount = new SavingsAccount();
                savingsAccount.DeleteSavingAccount(options.IdSavingsAccount);
            }
            else
            {
                Console.WriteLine("You can not create a new current account");
            }
        }
    }
}
    

