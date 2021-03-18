using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.RFIDObserver
{


    
    public interface IRFIDReader
    {
       
        event EventHandler<RFIDEventArgs> RfidChangedEvent;

        // Direct access to the current current value
        public int CardID { get; set; }

        


    }
}
