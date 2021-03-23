using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary.Logging
{
    public class Logging :ILogging
    {

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        
        public void LogLocked(DateTime timestamp, int id)
        {
            string log = timestamp.ToString() + "Dør låst med ID: " + id;
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(log);
            }
        }
        public void LogUnlocked(DateTime timestamp, int id)
        {
            string log = timestamp.ToString() + "Dør låst op med ID: " + id;
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(log);
            }
        }
    }
}
