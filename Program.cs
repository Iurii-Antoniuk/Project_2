using System;
using CommandLine;

namespace Project_2
{
    class Program
    {

       [Verb("withdraw", HelpText = "money withdraw")]
        class WithdrawOptions
        {
            [Option('a', "amount", Required = true, HelpText = "amount to withdraw")]
            public double Amount { get; set; }

            [Option('c', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

        }

        [Verb("transfer", HelpText = "money transaction")]
        class TransferOptions
        {

            [Option('t', "CToC", HelpText = "Transaction from a current account to a current account")]
            public bool CurrentToCurrent { get; set; }

            [Option('e', "CToS", HelpText = "Transaction from a current account to a saving account")]
            public bool CurrentToSaving { get; set; }

            [Option('r', "SToC", HelpText = "Transaction from a saving account to a current account")]
            public bool SavingToCurrent { get; set; }

        }

        [Verb("instant", HelpText = "Make a instant transaction")]
        class InstantTransferOptions : TransferOptions
        {
            [Option('c', "currentAccount", HelpText = "Id of the current account")]
            public int IdCurrentAccount { get; set; }

            [Option('s', "savingAccount", HelpText = "Id of the saving account")]
            public int IdSavingAccount { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount to transfer")]
            public double Amount { get; set; }
        }

        [Verb("delayed", HelpText = "Make a delayed transaction")]
        class DelayedTransferOptions : TransferOptions
        {
            [Option('c', "currentAccount", HelpText = "Id of the current account")]
            public int IdCurrentAccount { get; set; }

            [Option('s', "savingAccount", HelpText = "Id of the saving account")]
            public int IdSavingAccount { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount to transfer")]
            public double Amount { get; set; }

            [Option('d', "date", HelpText = "Execution date of the transfer")]
            public String DatetExe { get; set; }

        }
        [Verb("permanent", HelpText = "Make a permanent transaction")]
        class PermanentTransferOptions : TransferOptions
        {
            [Option('c', "currentAccount", HelpText = "Id of the current account")]
            public int IdCurrentAccount { get; set; }

            [Option('s', "savingAccount", HelpText = "Id of the saving account")]
            public int IdSavingAccount { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount to transfer")]
            public double Amount { get; set; }

            [Option('f', "first date", HelpText = "Date first execution of the transfer")]
            public String FirstExe { get; set; }

            [Option('l', "last date", HelpText = "Date last execution of the transfer")]
            public String LastExe { get; set; }

            [Option('i', "interval", HelpText = "interval between your execution")]
            public Int32 Interval { get; set; }

        }

        [Verb("createC", HelpText = "Client creation")]
        class CreateClientOptions
        {
            [Option('n', "name", Required = true, HelpText = "Client Name")]
            public string ClientName { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount")]
            public double Amount { get; set; }

            [Option('o',"overdraft", Required = true, HelpText ="overdraft of the current account")]
            public decimal Overdraft { get; set; }
        }

        [Verb("deleteC", HelpText = "Client deletion")]
        class DeleteClientOptions
        {
            [Option('i', "id", Required = true, HelpText = "id Client to delete")]
            public int IdClient { get; set; }
        }

        [Verb("modifyPass", HelpText ="Modify your password")]
        class ModifyPasssOptions
        {
            [Option('i', "id", Required = true, HelpText = "id Client to delete")]
            public int IdClient { get; set; }
        }

        [Verb("createSA", HelpText = "Create savings account")]
        class CreateSavingsAccountOptions
        {

            [Option('i', "id", Required = true, HelpText = "id Client")]
            public int IdClient { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount availble on the account")]
            public decimal Amount { get; set; }

            [Option('c', "ceiling", Required =true, HelpText ="account ceiling")]
            public decimal Ceiling { get; set; }
        }

        [Verb("deleteSA", HelpText = "Delete savings account")]
        class DeleteSavingsAccountOptions
        {
            [Option('i', "id", Required = true, HelpText = "id Savings Account")]
            public int IdSavingsAccount { get; set; }

        }

        [Verb("infoSavingAccounts", HelpText = "Get information about your account")]
        class InfoSavingAccountsOptions

        {
            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

            [Option('s', "id saving account", Required = true, HelpText = "Enter your saving account id")]
            public int IdSavingAccount { get; set; }
        }
        [Verb("infoCurrentAccounts", HelpText = "Get information about your current account")]
        class InfoCurrentAccountsOptions

        {
            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

            [Option('c', "id current account", HelpText = "Enter your current account id")]
            public int IdCurrentAccount { get; set; }

        }

        [Verb("infoUser", HelpText ="Get informations about your users")]
        class InfoUserOptions
        {
            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }
        }

        [Verb("infoTransaction", HelpText ="Get informations about a transaction")]
        class InfoTransactionOptions
        {
            [Option('d', "date", Required = true, HelpText ="Enter the date of the transaction")]
            public DateTime Date { get; set; }

            [Option('i', "client id", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }
        }

        [Verb("export", HelpText ="Export the informations about transactions")]
        class ExportOptions
        {
            
        }
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<WithdrawOptions, TransferOptions, InstantTransferOptions, DelayedTransferOptions, PermanentTransferOptions, CreateSavingsAccountOptions, DeleteSavingsAccountOptions,
                 InfoSavingAccountsOptions, InfoCurrentAccountsOptions, CreateClientOptions, DeleteClientOptions, ModifyPasssOptions, InfoUserOptions,
                 InfoTransactionOptions, ExportOptions>(args)

                .WithParsed<WithdrawOptions>(RunWithdrawOptions)
                //.WithParsed<TransferOptions>(RunTransferOptions)

                .WithParsed<InstantTransferOptions>(RunInstantTransferOptions)
                .WithParsed<DelayedTransferOptions>(RunDelayedTransferOptions)
                .WithParsed<PermanentTransferOptions>(RunPermanentTransferOptions)

                .WithParsed<CreateClientOptions>(RunCreateClientOptions)
                .WithParsed<DeleteClientOptions>(RunDeleteClientOptions)
                .WithParsed<ModifyPasssOptions>(RunModifyPassOptions)
                .WithParsed<CreateSavingsAccountOptions>(RunCreateSavingsAccountOptions)
                .WithParsed<DeleteSavingsAccountOptions>(RunDeleteSavingsAccountOptions)
                .WithParsed<InfoSavingAccountsOptions>(RunSavingAccountsInfoOptions)
                .WithParsed<InfoCurrentAccountsOptions>(RunCurrentAccountsInfoOptions)
                .WithParsed<InfoUserOptions>(RunInfoUserOptions)
                .WithParsed<InfoTransactionOptions>(RunInfoTransactionOptions)
                .WithParsed<ExportOptions>(RunExportOptions);

             ConnectionDB.GetConnectionString();

            //Administrator admin = new Administrator();
            //admin.CreateAdmin("admin");
            //admin.CreateClient("jus", 200);
            //Console.WriteLine(Person.Password);
          


            //string queryString = $"SELECT * FROM CurrentAccounts";
            //string queryString = $"SELECT * FROM \"Transaction\" ";
            //string queryString = $"SELECT * FROM \"Transaction\" WHERE id = 2 ";

            //SavingsAccount.AddInterest(10000);

            /*Authentification authentification = new Authentification();
            authentification.Login();
            Client client = new Client();
            client.AddFromBeneficiary(7001, 1003);*/

            //authentification.ModifyPassword();

            //Client client = new Client();
            //client.AddFromBeneficiary(200);

            //Client.WithdrawMoney(102, 20);
            //Client client = new Client();
            //client.ImmediateTransfer(100);

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
            trs.ExecuteDelayedTransfer(1);*/


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

        static void RunInstantTransferOptions(InstantTransferOptions options)
        {
            InstantTransfer instantTransfer = new InstantTransfer();

            if (options.CurrentToCurrent)
            {
                instantTransfer.RecordTransferFromCurrentToCurrent(options.IdCurrentAccount, options.Amount);
            }
            if(options.CurrentToSaving)
            {
                instantTransfer.RecordTransferFromCurrentToSaving(options.IdSavingAccount, options.Amount);
            }
            if(options.SavingToCurrent)
            {
                instantTransfer.RecordTransferFromSavingToCurrent(options.IdSavingAccount, options.Amount);
            }
        }
        static void RunDelayedTransferOptions(DelayedTransferOptions options)
        {
            DelayedTransfer delayedTransfer = new DelayedTransfer();

            if (options.CurrentToCurrent)
            {
                delayedTransfer.RecordTransferFromCurrentToCurrent(options.IdCurrentAccount, options.Amount, options.DatetExe);
            }
            if (options.CurrentToSaving)
            {
                delayedTransfer.RecordTransferFromCurrentToSaving(options.IdSavingAccount, options.Amount, options.DatetExe);
            }
            if (options.SavingToCurrent)
            {
                delayedTransfer.RecordTransferFromSavingToCurrent(options.IdSavingAccount, options.Amount, options.DatetExe);
            }
        }

        static void RunPermanentTransferOptions(PermanentTransferOptions options)
        {
            PermanentTransfer permanentTransfer = new PermanentTransfer();

            if (options.CurrentToCurrent)
            {
                permanentTransfer.RecordTransferFromCurrentToCurrent(options.IdCurrentAccount, options.Amount, options.FirstExe, options.LastExe, options.Interval);
            }
            if (options.CurrentToSaving)
            {
                permanentTransfer.RecordTransferFromCurrentToSaving(options.IdSavingAccount, options.Amount, options.FirstExe, options.LastExe, options.Interval);
            }
            if (options.SavingToCurrent)
            {
                permanentTransfer.RecordTransferFromSavingToCurrent(options.IdSavingAccount, options.Amount, options.FirstExe, options.LastExe, options.Interval);
            }
        }


        static void RunCreateClientOptions(CreateClientOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();

            if (id == 1)
            {
                Administrator administrator = new Administrator();
                administrator.CreateClient(options.ClientName, options.Amount, options.Overdraft);
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
            int id = authentification.Login();

            if (id == 1)
            {
                Administrator administrator = new Administrator();
                administrator.DeleteClient(options.IdClient);
            }
            else
            {
                Console.WriteLine("You can not delete a client");
            }
        }

        static void RunModifyPassOptions (ModifyPasssOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();
            if (id == options.IdClient)
            {
                authentification.ModifyPassword();
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
        }

        static void RunCreateSavingsAccountOptions(CreateSavingsAccountOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();

            if (id == 1)
            {
                SavingsAccount savingsAccount = new SavingsAccount();
                savingsAccount.CreateSavingAccount(options.IdClient, options.Amount, options.Ceiling);
            }
            else
            {
                Console.WriteLine("You can not create a new current account");
            }
        }

        static void RunDeleteSavingsAccountOptions(DeleteSavingsAccountOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();

            if (id == 1)
            {
                SavingsAccount savingsAccount = new SavingsAccount();
                savingsAccount.DeleteSavingAccount(options.IdSavingsAccount);
            }
            else
            {
                Console.WriteLine("You can not create a new saving account");
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
                Console.WriteLine("You can not access to users informations");
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

        static void RunExportOptions(ExportOptions options)
        {
            Authentification authentification = new Authentification();
            int id = authentification.Login();
            if (id == 1)
            {
                CSVFileExport.ExportCSVFile();
            }
            else
            {
                Console.WriteLine("You can not acces to the export of the transaction");
            }
        }
    }
}


    

