using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.DoorObserver;




namespace ClassLibrary
{
    public class Door : IDoor
    {
        private IDisplay _display = new Display.Display();
        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;
        public bool isDoorClosed { get; set; }
        public bool IsDoorLocked { get; set; }
        


        public Door()
        {
            IsDoorLocked = true;
            isDoorClosed = true;
        }

        public void CloseDoor()
        {
            if (!isDoorClosed)
            {
                isDoorClosed = true;
                Console.WriteLine("(Handling): Dør lukket");
                OnNewDoorState(new DoorChangedEventArgs { HasClosed = true });
            }
        }

        public void OpenDoor()
        {
            if (!IsDoorLocked && isDoorClosed)
            {
                isDoorClosed = false;
                Console.WriteLine("(Handling): Dør åben");
                OnNewDoorState(new DoorChangedEventArgs { HasClosed = false });
            }
            
        }

        public void LockDoor()
        {
            if (!IsDoorLocked && isDoorClosed)
            {
                IsDoorLocked = true;
                Console.WriteLine("(Handling): Dør låst");
            }

        }

        public void UnlockDoor()
        {
            if (IsDoorLocked)
            {
                IsDoorLocked = false;
                Console.WriteLine("(Handling): Dør låst op");
            }

        }
        private void OnNewDoorState(DoorChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
            // CurrentValueEvent?.Invoke(this, new CurrentEventArgs() {Current = this.CurrentValue});
        }
    }
}
