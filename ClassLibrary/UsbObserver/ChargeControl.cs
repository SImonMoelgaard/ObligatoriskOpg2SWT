using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using UsbSimulator;

namespace ClassLibrary.UsbObserver
{
    public class ChargeControl
    {
        

        public double Watt { get; set; }
        private IUsbCharger _charger { get; set; }

        public ChargeControl(IUsbCharger charger)
        {
            _charger = charger;
            charger.CurrentValueEvent += ChargeHandleEvent;
        }
      
        
        // Ændret til at chargecontrol kun står for charge
        public void StartCharging()
        {
            _charger.StartCharge();
        }
        public void StopCharging()
        {
            _charger.StopCharge();
        }

      

        public void ChargeHandleEvent(object sender, CurrentEventArgs chargingEvent)
        {


           
            
             Watt = chargingEvent.Current;
                if (Watt >0 && Watt<=5)
                {
                    StopCharging();
                }
                else if (Watt >=5 && Watt <= 500)
                {
                    StartCharging();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            
           
           
        }

    }
}
