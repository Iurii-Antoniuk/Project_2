using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace IKEAListenerr
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            /*ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);*/

            //Database.GetPendingTransactions();

            /*List<object[]> list = Database.GetPendingTransactions();

            foreach (object[] item in list)
            {
                Console.WriteLine(string.Join("\t\t", item));
            }
            Console.WriteLine("fini");*/

            //Database.GetPendingTransactionsFromDB();

            TransactionExecutor.ExecuteTransaction();

            Console.WriteLine("fini");

        }
    }
}
;