using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ClassLibrary.DoorObserver;




namespace ClassLibrary
{

    public class DoorStatusEventArgs : EventArgs
    {
        public bool IsClosed { get; set; }
    }


    public class Door : IDoor
    {
        private IDisplay _display = new Display.Display();
        public event EventHandler<DoorStatusEventArgs> DoorChangedEvent;

        public bool isDoorClosed { get; set; }
        public bool IsDoorLocked { get; set; }



        public Door()
        {
            IsDoorLocked = false;
            isDoorClosed = true;
            
        }

        

        public void CloseDoor()
        {
            if (!isDoorClosed)
            {
                isDoorClosed = true;
                Console.WriteLine("(Handling): Dør lukkes");
                OnNewDoorState(new DoorStatusEventArgs { IsClosed = true });
            }
        }

        public void OpenDoor()
        {
            if (!IsDoorLocked && isDoorClosed)
            {
                
                isDoorClosed = false;
                Console.WriteLine("(Handling): Dør åben");
                
                OnNewDoorState(new DoorStatusEventArgs() { IsClosed = isDoorClosed});
            }
            
        }

        public void LockDoor()
        {
            if (!IsDoorLocked && isDoorClosed)
            {
                IsDoorLocked = true;
            }

        }

        public void UnlockDoor()
        {
            if (IsDoorLocked)
            {
                IsDoorLocked = false;
                
            }

        }
        private void OnNewDoorState(DoorStatusEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
            // CurrentValueEvent?.Invoke(this, new CurrentEventArgs() {Current = this.CurrentValue});
        }
    }
}
