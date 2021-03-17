using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.RFIDObserver;

namespace ClassLibrary
{
    public class RfidReader : IRFIDReader
    {
        public event EventHandler<RFIDChangedEventArgs> RfidChangedEvent;
        public int RFID { get; set; }

       
        public void RFIDDetected(int id)
        {
            if (id != RFID)
            {
                OnNewRFIDState(new RFIDChangedEventArgs { CurrentRFIDEvent = id });
                RFID = id;
            }
           

        }

        protected  virtual void OnNewRFIDState(RFIDChangedEventArgs e)
        {
            RfidChangedEvent?.Invoke(this, e);
            // CurrentValueEvent?.Invoke(this, new CurrentEventArgs() {Current = this.CurrentValue});
        }
    }
}
