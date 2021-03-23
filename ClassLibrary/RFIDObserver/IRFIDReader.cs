using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.RFIDObserver
{


    
    public interface IRFIDReader
    {
       
        event EventHandler<RFIDEventArgs> RfidChangedEvent;

        
        public int CardID { get; set; }

        


    }
}
