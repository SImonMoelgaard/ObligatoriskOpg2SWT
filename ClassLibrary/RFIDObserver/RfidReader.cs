using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.RFIDObserver;

namespace ClassLibrary
{

    public class RFIDEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
    
    public class RfidReader : IRFIDReader
    {
        
        public event EventHandler<RFIDEventArgs> RfidChangedEvent;
        private int cardID;



        public int CardID
        {
            get { return cardID;}
            set
            {
                OnNewRFIDState(new RFIDEventArgs() { Id = value });
                cardID = value;
            }
        }
        
        protected  virtual void OnNewRFIDState(RFIDEventArgs e)
        {
            RfidChangedEvent?.Invoke(this, e);
            // CurrentValueEvent?.Invoke(this, new CurrentEventArgs() {Current = this.CurrentValue});
        }
    }
}
