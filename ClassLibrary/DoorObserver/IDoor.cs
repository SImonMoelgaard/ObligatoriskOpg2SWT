using System;
using System.Collections.Generic;
using System.Text;



namespace ClassLibrary.DoorObserver
{
    public  class DoorChangedEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public bool CurrentDoorEvent { set; get; }
    }

    public interface IDoor
    {

        

        event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        // Direct access to the current current value
        bool isDoorOpen  { get; }

        public void OnDoorClose();

        public void OnDoorOpen();


    }
}
