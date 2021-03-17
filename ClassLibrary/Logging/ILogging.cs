using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Logging
{
    public  interface ILogging
    {

        public void Log(string message, DateTime timestamp);


    }
}
