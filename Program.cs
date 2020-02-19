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
            [Option('a', "amount", Required = true, HelpText = "amount to withdraw")]
            public double Amount { get; set; }

            [Option('c', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

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

            [Option('t', "date time", HelpText = "Date where the transfer will happen")]
            public DateTime Date { get; set; }

            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

        }

        [Verb("createC", HelpText = "Client creation")]
        class CreateClientOptions : Options
        {
            [Option('n', "name", Required = true, HelpText = "Client Name")]
            public string ClientName { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount")]
            public double Amount { get; set; }
        }

        [Verb("deleteC", HelpText = "Client deletion")]
        class DeleteClientOptions : Options
        {
            [Option('i', "id", Required = true, HelpText = "id Client to delete")]
            public int IdClient { get; set; }
        }

        [Verb("createSA", HelpText = "Create savings account")]
        class CreateSavingsAccountOptions : Options
        {

            [Option('i', "id", Required = true, HelpText = "id Client")]
            public int IdClient { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount availble on the account")]
            public double Amount { get; set; }
        }

        [Verb("deleteSA", HelpText = "Delete savings account")]
        class DeleteSavingsAccountOptions : Options
        {
            [Option('i', "id", Required = true, HelpText = "id Savings Account")]
            public int IdSavingsAccount { get; set; }

        }

        [Verb("infoSavingAccounts", HelpText = "Get information about your account")]
        class InfoSavingAccountsOptions : Options

        {
            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

            [Option('s', "id saving account", Required = true, HelpText = "Enter your saving account id")]
            public int IdSavingAccount { get; set; }
        }
        [Verb("infoCurrentAccounts", HelpText = "Get information about your current account")]
        class InfoCurrentAccountsOptions : Options

        {
            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

            [Option('c', "id current account", HelpText = "Enter your current account id")]
            public int IdCurrentAccount { get; set; }

        }

        [Verb("infoUser", HelpText ="Get informations about your users")]
        class InfoUserOptions : Options
        {
            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }
        }

        [Verb("infoTransaction", HelpText ="Get informations about a transaction")]
        class InfoTransactionOptions : Options
        {
            [Option('d', "date", Required = true, HelpText ="Enter the date of the transaction")]
            public DateTime Date { get; set; }

            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }
        }
        static void Main(string[] args)
        {

           Parser.Default.ParseArguments<Options, WithdrawOptions, TransferOptions, CreateSavingsAccountOptions, DeleteSavingsAccountOptions, 
               InfoSavingAccountsOptions, InfoCurrentAccountsOptions, CreateClientOptions, DeleteClientOptions, InfoUserOptions, InfoTransactionOptions>(args)

              //.WithParsed<Options>(RunOptions)
              .WithParsed<WithdrawOptions>(RunWithdrawOptions)
              .WithParsed<TransferOptions>(RunTransferOptions)
              .WithParsed<CreateSavingsAccountOptions>(RunCreateSavingsAccountOptions)
              .WithParsed<DeleteSavingsAccountOptions>(RunDeleteSavingsAccountOptions)
              .WithParsed<InfoSavingAccountsOptions>(RunSavingAccountsInfoOptions)
              .WithParsed<InfoCurrentAccountsOptions>(RunCurrentAccountsInfoOptions)
              .WithParsed<InfoUserOptions>(RunInfoUserOptions)
              .WithParsed<InfoTransactionOptions>(RunInfoTransactionOptions)
              .WithParsed<CreateClientOptions>(RunCreateClientOptions)
              .WithParsed<DeleteClientOptions>(RunDeleteClientOptions);

            ConnectionDB.GetConnectionString();

            //DateTime date = new DateTime(2020, 2, 12);
            //Information.GetInfoByTransactionDate(date);

           // Console.WriteLine("Welcome on bank application");

            //string queryString = $"SELECT * FROM CurrentAccounts";
            //string queryString = $"SELECT * FROM \"Transaction\" ";
            //string queryString = $"SELECT * FROM \"Transaction\" WHERE id = 2 ";


            /*SavingsAccount.AddInterest(10000);

            Authentification authentification = new Authentification();
            authentification.Login();
            authentification.ModifyPassword();*/

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

            //Client client = new Client();
            //client.CheckCurrentAccount();
            //client.CheckSavingAccounts();
            //client.WithdrawMoney(-200);

            //Administrator admin = new Administrator();
            //admin.CreateClient("admin", 500);


            //int rows = ConnectionDB.CountRows("SavingAccounts");
            //Console.WriteLine(rows);
            //List<decimal> values = ConnectionDB.GetAccountColumnValues("SavingAccounts", "rate");
            //foreach (decimal value in values)
            //{
            //    Console.WriteLine(value);
            //}
            //Console.ReadLine();

            /*InstantTransfer ins = new InstantTransfer();
            ins.ImmediateTransfer(1);*/

            /*DelayedTransfer trs = new DelayedTransfer();
            trs.ExecuteDelayedTransfer(300);*/

            /*PermanentTransfer trp = new PermanentTransfer();
            trp.ExecutePermanentTransfer(300);*/


        }

        static void RunWithdrawOptions(WithdrawOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();

            if (id == options.IdClient)
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
            int id = authentification.Login();

            if (id == options.IdClient || id == 1)
            {
                if (options.Delayed)
                {
                    DelayedTransfer delayedTransfer = new DelayedTransfer();
                    delayedTransfer.ExecuteDelayedTransfer(options.Amount);
                }
                if (options.Instant)
                {
                    InstantTransfer instantTransfer = new InstantTransfer();
                    instantTransfer.ImmediateTransfer(options.Amount);
                }
                if (options.Permanent)
                {
                    PermanentTransfer permanentTransfer = new PermanentTransfer();
                    permanentTransfer.ExecutePermanentTransfer(options.Amount);
                }
            }
            else
            {
                Console.WriteLine("Wrong id");
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

        static void RunSavingAccountsInfoOptions(InfoSavingAccountsOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();
            if (id == options.IdClient || id==1)
            {
                Information.GetInfoBySavingsAccountId(options.IdSavingAccount);
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
        }

        static void RunCurrentAccountsInfoOptions(InfoCurrentAccountsOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();
            if (id == options.IdClient || id==1)
            {
                Information.GetInfoByCurrentAccountId(options.IdCurrentAccount);
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
        }

        static void RunInfoUserOptions (InfoUserOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();
            if (id == 1)
            {
                Information.GetInfoByUserId(options.IdClient);
            }
            else
            {
                Console.WriteLine("You can not access to the informations of users");
            }
        }

        static void RunInfoTransactionOptions(InfoTransactionOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();
            if (id == 1 || id==options.IdClient)
            {
                Information.GetInfoByTransactionDate(options.Date);
            }
            else
            {
                Console.WriteLine("Wrong input");
            }
        }
    }
}


    

