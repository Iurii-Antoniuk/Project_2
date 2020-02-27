using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IKEAListener
{
    public class Logger
    {
        private static Logger _instance = null;

        public StreamWriter Stream { get; set; }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;
            }
        }


        public void Debug(string message)
        {
            WriteToFile("DEBUG", message);
        }

        public void Info(string message)
        { 
            WriteToFile("INFO", message);
        }

        public void Error(string message)
        {
            WriteToFile("ERROR", message);
        }

        private void WriteToFile(string header, string message)
        {
            Stream.WriteLine("{0} - {1}: {2}", header, DateTime.Now, message);
        }
 
    }
}
