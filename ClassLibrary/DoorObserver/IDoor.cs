using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;


namespace ClassLibrary.DoorObserver
{
    
    public interface IDoor
    {
        public event EventHandler<DoorStatusEventArgs> DoorChangedEvent; 

        // Direct access to the current current value
        public bool isDoorClosed { get; set; }
        public bool IsDoorLocked { get; set; }

        public void CloseDoor();

        public void OpenDoor();

       public void LockDoor();
        public void UnlockDoor();
    }
}
