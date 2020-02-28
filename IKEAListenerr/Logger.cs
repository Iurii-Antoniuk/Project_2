using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace IKEAListenerr
{
    public class Logger
    {
        private static Logger _instance = null;

        public String Filepath { get; set; }

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

        public void WriteToFile(string header, string message)
        {
            String formattedMessage = String.Format("{0} - {1}: {2}", header, DateTime.Now, message);
            if (!File.Exists(Filepath))
            {
                using (StreamWriter sw = File.CreateText(Filepath))
                {
                    sw.WriteLine(formattedMessage);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(Filepath))
                {
                    sw.WriteLine(formattedMessage);
                }
            }
        }

    }
}
