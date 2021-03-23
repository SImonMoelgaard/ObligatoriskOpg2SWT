using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Logging
{
    public  interface ILogging
    {

        void LogLocked(DateTime timestamp, int id);
        void LogUnlocked(DateTime timestamp, int id);


    }
}
