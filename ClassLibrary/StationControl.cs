﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters;
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
        private bool doorIsLocked;
        private int rfidID;
        private bool charging = false;
        
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

            
            _rfid.RfidChangedEvent += RFIDDetectedHandle;
            _door.DoorChangedEvent += DoorEventHandle;
            _usbCharger.CurrentValueEvent += ChargerHandle;


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

        public int oldID
        {
            get { return _oldID; }
            set { _oldID = value; }
        }

        // Her mangler de andre trigger handlere
        private void RFIDDetectedHandle(object o, RFIDEventArgs rfid)
        {

            if (_state== ChargingStationState.Available && charging ==false)
            {
                if (_usbCharger.Connected)
                {
                    message = $"ID: {rfid.Id}: Låser dør og starter ladning - Ladeskab optaget";
                    _display.PrintMessage(message);
                    _oldID = rfid.Id;
                    _door.LockDoor();
                    _chargeControl.StartCharging();
                    
                    _state = ChargingStationState.Locked;
                    charging = true;

                }
                
                else if (_usbCharger.Connected==false)
                {
                    message = "Tilslutningsfejl. Sørg for at telefonen er tilsluttet";
                    _display.PrintMessage(message);
                }
                else if (_door.isDoorClosed ==false)
                {
                    message = "Luk døren før indlæsning af RFID";
                }


            }
            else if (charging)
            {
                if (rfid.Id == _oldID)
                {
                    _chargeControl.StopCharging();
                    message = $"ID godkendt: {rfid.Id} Ladning stoppet. Fjern telefon";
                    _door.UnlockDoor();
                    _state = ChargingStationState.Available;
                    _display.PrintMessage(message);
                    charging = false;
                }
                else if (_oldID!=rfid.Id)
                {
                    message = "RFID fejl. Prøv igen";
                    _display.PrintMessage(message);
                }
                
                

            }
            

        }

        private void DoorEventHandle(object o, DoorStatusEventArgs door)
        {
            
            switch (door.IsClosed)
            {
                case true:
                    if (_state == ChargingStationState.Opened)
                    {
                        if (_usbCharger.Connected)
                        {
                            message = "Indlæs RFID";
                            doorIsLocked = true;
                            //_door.LockDoor();
                            //_chargeControl.StartCharging();
                            _state = ChargingStationState.Available;
                            //message = "Starter ladning";
                        }
                        else
                        {
                            message = "Tilsut telefon";
                        }

                    } ;
                    break;
                case false:
                    _state = ChargingStationState.Opened;
                    message = "Dør åbnet. Tilslut venligst telefonen";
                    break;
                
            }
            _display.PrintMessage(message);

        }

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
