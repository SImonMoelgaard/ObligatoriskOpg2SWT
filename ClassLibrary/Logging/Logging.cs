using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary.Logging
{
    public class Logging :ILogging
    {

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        
        public void Log(DateTime timestamp, int id, string message)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(timestamp + " : " + id + " : " + message);
            }
        }
    }
}
