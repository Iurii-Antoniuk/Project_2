﻿using System;
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
       /* class Options

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

        [Verb("info", HelpText = "Get information")]
        class InfoOptions : Options

        {
            [Option('i', "client id", HelpText = "Enter your client id")]
            public int IdClient { get; set; }

            [Option('c', "id current account", HelpText = "Enter your current account id")]
            public int IdCurrentAccount { get; set; }
            [Option('s', "id saving account", HelpText = "Enter your saving account id")]
            public int IdSavingAccount { get; set; }
        }*/

        static void Main(string[] args)
        {

           /* Parser.Default.ParseArguments<Options, WithdrawOptions, TransferOptions, AccountOptions, InfoOptions, ClientOptions>(args)
             .WithParsed<Options>(RunOptions)
             .WithParsed<WithdrawOptions>(RunWithdrawOptions)
             .WithParsed<TransferOptions>(RunTransferOptions)
             .WithParsed<AccountOptions>(RunAccountOptions)
             .WithParsed<InfoOptions>(RunInfoOptions)
             .WithParsed<ClientOptions>(RunClientOptions);*/

            //Console.WriteLine("Welcome on bank application");


            //Authentification authentification = new Authentification();
            //authentification.Login();
            //authentification.ModifyPassword(3);

            //ConnectionDB.GetConnectionString();

            Administrator admin = new Administrator();
            //admin.CreateClient("Claire", 3000);
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

            Client client = new Client();


            //client.WithdrawMoney(3, 500);

            //client.ImmediateTransfer(1);
            //client.DelayedTransfer(1);
            //Transaction.CheckDate();
            DelayedTransfer.ExecuteDelayedTransfer(1);
        
        }
        /*

        static void RunOptions(Options options)
        {

        }
        static void RunInfoOptions(InfoOptions options)
        {
            Console.WriteLine("Enter your password  : ");
            string password = Console.ReadLine();
            Client client = new Client();
        }



        static void RunWithdrawOptions(WithdrawOptions options)
        {
            //Client.WithdrawMoney2(options.AccountId, options.Amount);
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
            if (options.CreateClient)
            {

            }
            if (options.DeleteClient)
            {

            }
        }*/
    }
}
    

