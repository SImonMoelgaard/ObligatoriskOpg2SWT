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
        public bool isDoorOpen { get; set; }
        
        public void DoorClose()
        {
            isDoorOpen = false;

        }

        public void DoorOpen()
        {
            isDoorOpen = true;
        }
        private void OnNewDoorState()
        {
            DoorChangedEvent?.Invoke(this, new DoorChangedEventArgs() { CurrentDoorEvent = this.isDoorOpen});
            // CurrentValueEvent?.Invoke(this, new CurrentEventArgs() {Current = this.CurrentValue});
        }
    }
}
