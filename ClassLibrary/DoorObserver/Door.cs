using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.DoorObserver;



namespace ClassLibrary
{
    public class Door : IDoor
    {
        
        public event EventHandler<IDoor.DoorChangedEventArgs> DoorChangedEvent;
        public bool isDoorOpen { get; }
        public void OnDoorClose()
        {
            throw new NotImplementedException();
        }

        public void OnDoorOpen()
        {
            throw new NotImplementedException();
        }
        private void OnNewCurrent()
        {
            DoorChangedEvent?.Invoke(this, new DoorChangedEventArgs() { is = this.CurrentValue });
            // CurrentValueEvent?.Invoke(this, new CurrentEventArgs() {Current = this.CurrentValue});
        }
    }
}
