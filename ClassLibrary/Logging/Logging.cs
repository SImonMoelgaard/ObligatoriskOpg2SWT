using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary.Logging
{
    public class Logging :ILogging
    {
        public int ID { get; set; }


        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        public void LogDoorLocked(int id)

        {
            ID = id;
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", ID);
            }
        }

        public void LogDoorUnlocked(int id)
        {
            ID = id;
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            }
        }
    }
}
