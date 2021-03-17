using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using ClassLibrary.DoorObserver;
using ClassLibrary.Logging;
using ClassLibrary.RFIDObserver;
using ClassLibrary.UsbObserver;
using UsbSimulator;

namespace Ladeskab
{
    public class StationControl
    {
        private IDisplay _display;
        private IDoor _door;
        private IRFIDReader _rfid;
        private IUsbCharger _usbCharger;
        private ChargeControl _chargeControl;
        private ILogging _logging;

        private ChargingStationState _state;
        private int _oldID;
        private string message = "";

        public DateTime TimeStamp { get; set; }
        public double Watt { get; set; }
        public ChargingStationState state
        {
            get => _state;
            set => _state = value;
        }
        



        public StationControl(IDisplay display, IDoor door, IRFIDReader rfid, IUsbCharger usbcharger,
            ChargeControl chargeControl, ILogging logging)
        {
            _display = display;
            _door = door;
            _rfid = rfid;
            _usbCharger = usbcharger;
            _chargeControl = chargeControl;
            _logging = logging;

            _usbCharger.CurrentValueEvent += ChargerHandle;
            _rfid.RfidChangedEvent += RFIDDetectedHandle;
            _door.DoorChangedEvent += DoorClosedHandle;


            //Vi sætter oplysninger, så skabet er ledigt.
            _state = ChargingStationState.Available;
            _usbCharger.Connected = false;
            _oldID = 0;
        }

        public enum ChargingStationState
        {
            Available,
            Locked,
            Opened
        }

        // Her mangler de andre trigger handlere
        private void RFIDDetectedHandle(object o, RFIDChangedEventArgs rfid)
        {}
        private void DoorClosedHandle(object o, DoorChangedEventArgs door)
        {}

        private void ChargerHandle(object o, CurrentEventArgs charger)
        {
            Watt = charger.Current;
            if (Watt >0&&Watt<=5)
            {
                message = "Telefon opladet";
            }
            else if (Watt>=6)
            {
                message = "Telefon oplader";
            }
            _display.PrintMessage(message);

        }
    }
}
