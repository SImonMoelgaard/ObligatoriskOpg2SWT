using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Logging
{
    public  interface ILogging
    {

        public int ID { get; set; }
        void LogDoorLocked(int id);

        void LogDoorUnlocked(int id);


    }
}
