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

            /*Administrator admin = new Administrator();
            admin.CreateClient("choupi", 500); */
            //Client.WithdrawMoney2(101, 100);

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
