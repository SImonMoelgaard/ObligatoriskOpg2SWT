using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.DoorObserver
{
    public class DoorChangedEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public bool CurrentDoorEvent { set; get; }
    }
    public interface IDoor
    {
        

        // Direct access to the current current value
        bool isDoorOpen { get; }

        void OnDoorClose();

        void OnDoorOpen();
    }
}
