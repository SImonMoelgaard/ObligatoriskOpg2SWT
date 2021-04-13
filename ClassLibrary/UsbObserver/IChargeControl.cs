using System;
using System.Collections.Generic;
using System.Text;
using UsbSimulator;

namespace ClassLibrary.UsbObserver
{
    public interface IChargeControl
    {
        public void StartCharging();
        


        public void StopCharging();

        public void ChargeHandleEvent(object sender, CurrentEventArgs chargingEvent);


    }
}
