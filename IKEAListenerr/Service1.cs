using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Data.Sql;
using System.Configuration;
using System.Data.SqlClient;

namespace IKEAListenerr
{
    public partial class Service1 : ServiceBase
    {
        private List<Timer> _timers = null;
        private Logger _logger = null;

        public Service1()
        {
            InitializeComponent();
            _timers = new List<Timer>();
            _logger = Logger.Instance;
            _logger.Filepath = System.AppDomain.CurrentDomain.BaseDirectory + @"\IKEAListener.log";
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("Service started");
            AddEventHandler(ListenTransactions, TimeSpan.FromSeconds(10));
            AddEventHandler(ListenInterest, TimeSpan.FromHours(1));
        }

        

        private void AddEventHandler(ElapsedEventHandler handler, TimeSpan timeSpan)
        {
            _logger.Info("Adding event handler " + handler);

            Timer timer = new Timer();
            timer.Elapsed += handler;
            timer.Interval = timeSpan.TotalMilliseconds; //number in milisecinds  
            timer.Enabled = true;
            _timers.Add(timer);
        }

        private void ListenInterest(object source, ElapsedEventArgs e)
        {
            _logger.Info("Listening interest");
        }

        private void ListenTransactions(object source, ElapsedEventArgs e)
        {
            try
            {
                _logger.Info("Executing transaction execution");
                TransactionExecutor.ExecuteTransaction();
            }
            catch(Exception exception)
            {
                String message = exception.Message + "\n" + exception.StackTrace;
                _logger.Error(message);
            }
        }

        protected override void OnStop()
        {
            _logger.Info("Stopping service");
        }

        /*
        private static void InterestAddition(object source, ElapsedEventArgs e)
        {
            int numberOFAccounts = CountRows("SavingAccounts");
            if (numberOFAccounts > 0)
            {
                UpdateSavingAmounts();
            }
        }

        
        public static void UpdateSavingAmounts()
        {
            int numberOFAccounts = CountRows("SavingAccounts");
            SqlConnection connection = new SqlConnection(ConnectionDB.GetConnectionString());
            connection.Open();
            for (int i = 0; i < numberOFAccounts; i++)
            {
                string queryString = $"UPDATE SavingAccounts SET amount = (amount + amount * rate) WHERE id = 100 + {i};";
                SqlCommand command = new SqlCommand(queryString, connection);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public static int CountRows(string tableName)
        {
            string queryString = $"SELECT COUNT(*) FROM {tableName}; ";
            int numberRows = ConnectionDB.ReturnID(queryString);
            return numberRows;
        }

        public static void DoWithdrawal()
        {
            //Regarder dans la base si pending
            //Regarder si méthode withdraw

            string queryWithdraw = $"";
        }
        */
    }
}
