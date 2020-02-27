using System;
using CommandLine;

namespace IKEACmdUtil
{
    class Program
    {

       [Verb("withdraw", HelpText = "money withdraw")]
        class WithdrawOptions : LoginOptions
        {
            [Option('a', "amount", Required = true, HelpText = "amount to withdraw")]
            public double Amount { get; set; }

           [Option('c', "clientId", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

        }

       
        class TransferOptions : LoginOptions
        {
            [Option('t', "CToC", HelpText = "Transaction from a current account to a current account")]
            public bool CurrentToCurrent { get; set; }

            [Option('e', "CToS", HelpText = "Transaction from a current account to a saving account")]
            public bool CurrentToSaving { get; set; }

            [Option('r', "SToC", HelpText = "Transaction from a saving account to a current account")]
            public bool SavingToCurrent { get; set; }
         
            [Option("emitter", Required = true, HelpText = "Id of the emitter account")]
            public int EmitterId { get; set; }

            [Option("beneficiary", Required = true, HelpText = "Id of the beneficiary account")]
            public int BeneficiaryId { get; set; }

            [Option("amount", Required = true, HelpText = "amount to transfer")]
            public double Amount { get; set; }

            [Option('c', "clientId", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }

        }

        [Verb("instant", HelpText = "Make a instant transaction")]
        class InstantTransferOptions : TransferOptions
        {

        }

        [Verb("delayed", HelpText = "Make a delayed transaction")]
        class DelayedTransferOptions : TransferOptions
        {
            [Option('d', "date", Required = true, HelpText = "Execution date of the transfer")]
            public String DatetExe { get; set; }   
        }

        [Verb("permanent", HelpText = "Make a permanent transaction")]
        class PermanentTransferOptions : TransferOptions
        {
            [Option('f', "first", Required = true, HelpText = "Date first execution of the transfer")]
            public String FirstExe { get; set; }

            [Option('l', "last", Required = true, HelpText = "Date last execution of the transfer")]
            public String LastExe { get; set; }

            [Option('i', "interval", Required = true, HelpText = "interval (days) between your execution")]
            public Int32 Interval { get; set; }

        }

        [Verb("createC", HelpText = "Client creation")]
        class CreateClientOptions : LoginOptions
        {
            [Option('n', "name", Required = true, HelpText = "Client Name")]
            public string ClientName { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount")]
            public double Amount { get; set; }

            [Option('o',"overdraft", Required = true, HelpText ="overdraft of the current account")]
            public decimal Overdraft { get; set; }
        }

        [Verb("deleteC", HelpText = "Client deletion")]
        class DeleteClientOptions : LoginOptions
        {
            [Option('i', "id", Required = true, HelpText = "id Client to delete")]
            public int IdClient { get; set; }

            [Option('d',"destinaryAccount", Required =true,HelpText ="destinary account")]
            public int DestinaryAccount { get; set; }
        }

        [Verb("modifyPass", HelpText ="Modify your password")]
        class ModifyPasssOptions : LoginOptions
        {
            [Option('i', "id", Required = true, HelpText = "id Client")]
            public int IdClient { get; set; }
        }

        [Verb("createSA", HelpText = "Create savings account")]
        class CreateSavingsAccountOptions : LoginOptions
        {

            [Option('i', "id", Required = true, HelpText = "id Client")]
            public int IdClient { get; set; }

            [Option('a', "amount", Required = true, HelpText = "amount availble on the account")]
            public decimal Amount { get; set; }

            [Option('c', "ceiling", Required =true, HelpText ="account ceiling")]
            public decimal Ceiling { get; set; }
        }

        [Verb("deleteSA", HelpText = "Delete savings account")]
        class DeleteSavingsAccountOptions : LoginOptions
        {
            [Option('i', "id", Required = true, HelpText = "id Savings Account")]
            public int IdSavingsAccount { get; set; }

        }

        [Verb("infoSavingAccounts", HelpText = "Get information about your account")]
        class InfoSavingAccountsOptions : LoginOptions

        {
            [Option('s', "idSA", Required = true, HelpText = "Enter your saving account id")]
            public int IdSavingAccount { get; set; }
        }

        [Verb("infoCurrentAccounts", HelpText = "Get information about your current account")]
        class InfoCurrentAccountsOptions : LoginOptions
        {
            [Option('c', "idCA", Required = true, HelpText = "Enter your current account id")]
            public int IdCurrentAccount { get; set; }
        }

        [Verb("infoUser", HelpText ="Get informations about your users")]
        class InfoUserOptions : LoginOptions
        {
            [Option('i', "clientId", Required = true, HelpText = "Enter your client id")]
            public int IdClient { get; set; }
        }

        [Verb("infoTransaction", HelpText ="Get informations about a transaction")]
        class InfoTransactionOptions : LoginOptions
        {
            [Option('d', "date", Required = true, HelpText ="Enter the date of the transaction")]
            public string Date { get; set; }

            [Option('i', "clientId", HelpText = "Enter your client id")]
            public int IdClient { get; set; }
        }

        [Verb("export", HelpText ="Export the informations about transactions")]
        class ExportOptions : LoginOptions
        {
            [Option('p', "path", Required = true, HelpText ="Path to save the csv file")]
            public string Path { get; set; }
        }

        class LoginOptions
        {
            [Option('u', "user", Required = true, HelpText = "User name")]
            public String UserName { get; set; }
        }

        [Verb("addDonators", HelpText = "Add a donator")]
        class AddDonatorsOptions : LoginOptions
        {
            [Option('d', "donator", Required = true, HelpText = "Current account id of donator" )]
            public int Donator { get; set; }
            [Option('i', "clientId", Required = true, HelpText = "Enter your client id")]
            public int ClientId { get; set; }
        }

        static void Main(string[] args)
        {   
            Parser.Default.ParseArguments<LoginOptions, WithdrawOptions, InstantTransferOptions, DelayedTransferOptions, 
                PermanentTransferOptions, CreateSavingsAccountOptions, DeleteSavingsAccountOptions, InfoSavingAccountsOptions, 
                InfoCurrentAccountsOptions, CreateClientOptions, DeleteClientOptions, ModifyPasssOptions, InfoUserOptions,
                 InfoTransactionOptions, ExportOptions, AddDonatorsOptions>(args)
                 .WithParsed<LoginOptions>(RunLoginUser)
                .WithParsed<WithdrawOptions>(RunWithdrawOptions)
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
                .WithParsed<ExportOptions>(RunExportOptions)
                .WithParsed<AddDonatorsOptions>(RunAddDonatorOptions);

            ConnectionDB.GetConnectionString();
        }

        static void RunLoginUser(LoginOptions loginOptions)
        {
            Authentification.Login(loginOptions.UserName);
        }

        static void RunWithdrawOptions(WithdrawOptions options)
        {
            if (Person.ID == options.IdClient)
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

            if (Person.ID == options.IdClient)
            {
                if (options.CurrentToCurrent)
                {
                    instantTransfer.RecordTransferFromCurrentToCurrent(options.EmitterId, options.BeneficiaryId, options.Amount);
                }
                if (options.CurrentToSaving)
                {
                    instantTransfer.RecordTransferFromCurrentToSaving(options.EmitterId, options.BeneficiaryId, options.Amount);
                }
                if (options.SavingToCurrent)
                {
                    instantTransfer.RecordTransferFromSavingToCurrent(options.EmitterId, options.BeneficiaryId, options.Amount);
                }
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
        }
        static void RunDelayedTransferOptions(DelayedTransferOptions options)
        {
            DelayedTransfer delayedTransfer = new DelayedTransfer();

            if (Person.ID == options.IdClient)
            {
                if (options.CurrentToCurrent)
                {
                    delayedTransfer.RecordTransferFromCurrentToCurrent(options.EmitterId, options.BeneficiaryId, options.Amount, options.DatetExe);
                }
                if (options.CurrentToSaving)
                {
                    delayedTransfer.RecordTransferFromCurrentToSaving(options.EmitterId, options.BeneficiaryId, options.Amount, options.DatetExe);
                }
                if (options.SavingToCurrent)
                {
                    delayedTransfer.RecordTransferFromSavingToCurrent(options.EmitterId, options.BeneficiaryId, options.Amount, options.DatetExe);
                }
            }
            else
            {
                Console.WriteLine("Wrong id");
            }

        }

        static void RunPermanentTransferOptions(PermanentTransferOptions options)
        {
            PermanentTransfer permanentTransfer = new PermanentTransfer();

            if (Person.ID == options.IdClient)
            {
                if (options.CurrentToCurrent)
                {
                    permanentTransfer.RecordTransferFromCurrentToCurrent(options.EmitterId, options.BeneficiaryId, options.Amount, options.FirstExe, options.LastExe, options.Interval);
                }
                if (options.CurrentToSaving)
                {
                    permanentTransfer.RecordTransferFromCurrentToSaving(options.EmitterId, options.BeneficiaryId, options.Amount, options.FirstExe, options.LastExe, options.Interval);
                }
                if (options.SavingToCurrent)
                {
                    permanentTransfer.RecordTransferFromSavingToCurrent(options.EmitterId, options.BeneficiaryId, options.Amount, options.FirstExe, options.LastExe, options.Interval);
                }
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
        }


        static void RunCreateClientOptions(CreateClientOptions options)
        {
            if (Person.ID == 1)
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
            if (Person.ID == 1)
            {
                Administrator administrator = new Administrator();
                administrator.DeleteClient(options.IdClient, options.DestinaryAccount);
            }
            else
            {
                Console.WriteLine("You can not delete a client");
            }
        }

        static void RunModifyPassOptions (ModifyPasssOptions options)
        {
            if (Person.ID == options.IdClient)
            {
                Authentification.ModifyPassword();
            }
            else
            {
                Console.WriteLine("Wrong id");
            }
        }

        static void RunCreateSavingsAccountOptions(CreateSavingsAccountOptions options)
        {
            if (Person.ID == 1)
            {
                SavingsAccount savingsAccount = new SavingsAccount();
                savingsAccount.CreateSavingAccount(options.IdClient, options.Amount, options.Ceiling);
            }
            else
            {
                Console.WriteLine("You can not create a new saving account");
            }
        }

        static void RunDeleteSavingsAccountOptions(DeleteSavingsAccountOptions options)
        {
            if (Person.ID == 1)
            {
                SavingsAccount savingsAccount = new SavingsAccount();
                savingsAccount.DeleteSavingAccount(options.IdSavingsAccount);
            }
            else
            {
                Console.WriteLine("You can not delete an existing saving account");
            }
        }

        static void RunSavingAccountsInfoOptions(InfoSavingAccountsOptions options)
        {
            int ownerId = Information.GetAccountOwnerId(options.IdSavingAccount, AccountType.Saving);
            if (Person.ID == ownerId || Person.ID == 1)
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
            int ownerId = Information.GetAccountOwnerId(options.IdCurrentAccount, AccountType.Current);
            if (Person.ID == ownerId || Person.ID == 1)
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
            if (Person.ID == 1)
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
            if (Person.ID == 1 || Person.ID == options.IdClient)
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
            if (Person.ID == 1)
            {
                CSVFileExport.ExportCSVFile(options.Path);
            }
            else
            {
                Console.WriteLine("You can not acces to the export of the transaction");
            }
        }

        static void RunAddDonatorOptions(AddDonatorsOptions options)
        {
            if (Person.ID == options.ClientId)
            {
                SavingsAccount.AddDonators(options.Donator);
            }
            else
            {
                Console.WriteLine("Donator is invalid");
            }
        }
    }
}


    

