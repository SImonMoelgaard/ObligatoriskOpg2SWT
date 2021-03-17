using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.RFIDObserver
{


    public class RFIDChangedEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public int CurrentRFIDEvent { set; get; }
    }
    public interface IRFIDReader
    {
       
        event EventHandler<RFIDChangedEventArgs> RfidChangedEvent;

        // Direct access to the current current value
        int RFID { get; set; }

        void RFIDDetected(int id);


    }
}
