using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;
using ClassLibrary.DoorObserver;
using ClassLibrary.Logging;
using ClassLibrary.RFIDObserver;
using ClassLibrary.UsbObserver;
using Ladeskab;
using NUnit.Framework;
using UsbSimulator;
using NSubstitute;

namespace Nunit.TestProject
{
    public class TestStationControl
    {

        private StationControl _uut;
        private IDisplay _display;
        private ILogging _log;
        private IDoor _door;
        private IRFIDReader _RFID;
        private IUsbCharger _UsbCharger;
        private ChargeControl _CC;

        [SetUp]
        public void setup()
        {
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _RFID = Substitute.For<IRFIDReader>();
            _UsbCharger = Substitute.For<IUsbCharger>();
            _CC = new ChargeControl(_UsbCharger);
            _log = Substitute.For<ILogging>();
            _uut = new StationControl(_display, _door,_RFID,_UsbCharger, _CC, _log);
            
        }

        //Test af Handle Door

        [Test]
        public void DoorIsClosed_UsbChargerConnected()
        {
            _uut.state = StationControl.ChargingStationState.Opened;
            _UsbCharger.Connected = true;
            _door.DoorChangedEvent += Raise.EventWith(this, new DoorStatusEventArgs()
            {
                IsClosed = true
            });

            _display.Received(1).PrintMessage("Indlæs RFID");
        }

        [Test]
        public void DoorIsClosed_UsbChargerNotConnected()
        {
            _uut.state = 
            _uut.state = StationControl.ChargingStationState.Opened;
            _UsbCharger.Connected = false;
            _door.DoorChangedEvent += Raise.EventWith(this, new DoorStatusEventArgs()
            {
                IsClosed = true
            });

            _display.Received(1).PrintMessage("Tilslut telefon");
        }
        [Test]
        public void DoorIsOpen_PleaseConnectCharger()
        {
            _uut.state = StationControl.ChargingStationState.Opened;
            _UsbCharger.Connected = false;
            _door.DoorChangedEvent += Raise.EventWith(this, new DoorStatusEventArgs()
            {
                IsClosed = false
            });

            _display.Received(1).PrintMessage("Dør åbnet. Tilslut venligst telefonen");
        }
        //RFID Handle TEST
        [Test]
        public void RFID_Available_NotCharging_ConnectedChargerTest()
        {
            _uut.state = StationControl.ChargingStationState.Available;
            _uut.charging = false;
            _UsbCharger.Connected = true;
            _door.isDoorClosed = true;

            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs(){Id = 1});
            _display.Received(1).PrintMessage("ID: 1: Låser dør og starter ladning - Ladeskab optaget");
        }

        [Test]
        public void RFID_Available_NotCharging_NotConnectedChargerTest()
        {
            _uut.state = StationControl.ChargingStationState.Available;
            _uut.charging = false;
            _UsbCharger.Connected =false;
            _door.isDoorClosed = true;

            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs() { Id = 1 });
            _display.Received(1).PrintMessage("Tilslutningsfejl. Sørg for at telefonen er tilsluttet");
        }

        [Test]
        public void RFID_Available_NotCharging_DoorOpen()
        {
            _uut.charging = false;
            _uut.state = StationControl.ChargingStationState.Available;
            _door.isDoorClosed = false;
            _UsbCharger.Connected = false;
            //_UsbCharger.Connected = true;

            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs() { Id = 1 });
            _display.Received(1).PrintMessage("Luk døren først");
        }

        [Test]
        public void UnlockCheck_Ischarging_ID_oldID_right()
        {
            _uut.oldID = 1;
            _uut.charging = true;
            _uut.state = StationControl.ChargingStationState.Available;
            
            
            //_UsbCharger.Connected = true;

            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs() { Id =1 });
            _display.Received(1).PrintMessage("ID godkendt: 1 Ladning stoppet. Fjern telefon");

        }
        public void UnlockCheck_Ischarging_ID_oldID_Wrong()
        {
            _uut.oldID = 2;
            _uut.charging = true;
            _uut.state = StationControl.ChargingStationState.Available;


            //_UsbCharger.Connected = true;

            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs() { Id = 1 });
            _display.Received(1).PrintMessage("RFID fejl. Prøv igen");

        }

        //Test af ChargerHandle
        [Test]
        public void ChargingPhone_Done()
        {

            _UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() {Current = 5});
            _display.Received(1).PrintMessage("Telefon opladet");
        }
        [Test]
        public void ChargingPhone()
        {

            _UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 6 });
            _display.Received(1).PrintMessage("Telefon oplader");
        }


        [Test]
        public void WattSet()
        {
            _uut.Watt = 20;
            Assert.That(_uut.Watt, Is.EqualTo(20));
        }

        [Test]
        public void TEstOldIDGet()
        {
            _uut.oldID = 10;

            Assert.That(_uut.oldID, Is.EqualTo(10));
        }

        [Test]
        public void TestGetChargingstationState()
        {
            
            Assert.That(_uut.charging, Is.EqualTo(false));
        }

        [Test]
        public void SetDatetime()
        {
            _uut.TimeStamp = DateTime.Today;
            Assert.That(_uut.TimeStamp, Is.EqualTo(DateTime.Today));
        }

        [Test]
        public void WattGet()
        {
            
        }
    }
}
