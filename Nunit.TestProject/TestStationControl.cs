using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using ClassLibrary;
using ClassLibrary.DoorObserver;
using ClassLibrary.Logging;
using ClassLibrary.RFIDObserver;
using ClassLibrary.UsbObserver;
using Ladeskab;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using UsbSimulator;
using NSubstitute;
using NUnit.Framework.Internal;

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
        public void Setup()
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
        public void DoorGetsClosed_UsbChargerConnected()
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
        [Test]
        public void DoorIsOpen_PhoneConnected()
        {
            _uut.state = StationControl.ChargingStationState.Opened;
            _UsbCharger.Connected = true;
            _door.DoorChangedEvent += Raise.EventWith(this, new DoorStatusEventArgs()
            {
                IsClosed = false
            });
            _display.Received(1).PrintMessage("Dør åbnet. Luk døren først");
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
            _uut.state = StationControl.ChargingStationState.Available;
            _uut.charging = false;
            _UsbCharger.Connected = true;
            _door.isDoorClosed = true;
            
            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs(){Id = 1});

            _uut.charging = true;
            _uut.state = StationControl.ChargingStationState.Available;
            
            
            //_UsbCharger.Connected = true;

            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs() { Id =1 });
            _display.Received(1).PrintMessage("ID godkendt: 1 Ladning stoppet. Åben venligst døren, Fjern telefonen og luk døren efter Dem");

        }
        [Test]
        public void UnlockCheck_Ischarging_ID_oldID_Wrong()
        {

            _uut.state = StationControl.ChargingStationState.Available;
            _uut.charging = false;
            _UsbCharger.Connected = true;
            _door.isDoorClosed = true;
            
            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs(){Id = 2});


            //_uut.oldID = 2;
            _uut.charging = true;
            _uut.state = StationControl.ChargingStationState.Available;


            //_UsbCharger.Connected = true;

            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs() { Id = 1 });
            _display.Received(1).PrintMessage("RFID fejl. Prøv igen");

        }

        //Test af ChargerHandle
        [Test]
        public void InvalidChargingValuewith0()
        {
            Assert.Throws<InvalidOperationException>(() => _UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 0 }));
        }

        [Test]
        public void ChargingPhone_Done_with1()
        {

            _UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() {Current = 1});
            _display.Received(1).PrintMessage("Telefon opladet");
        }

        [Test]
        public void ChargingPhone_Done_with5()
        {

            _UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() {Current = 5});
            _display.Received(1).PrintMessage("Telefon opladet");
        }
        
        [Test]
        public void InvalidChargingValueTwo()
        {
            Assert.Throws<InvalidOperationException>(() => _UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 6 }));
        }

        [Test]
        public void InvalidChargingValue()
        {
            Assert.Throws<InvalidOperationException>(()=>_UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 500 }));
        }

        [Test]
        public void ChargingPhone()
        {

            _UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 501 });
            _display.Received(1).PrintMessage("Telefon oplader");
        }

        
        

        [Test]
        public void WattGet()
        {

            _UsbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 550 });
            Assert.That(_uut.Watt, Is.EqualTo(550));
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
            _uut.state = StationControl.ChargingStationState.Available;
            _uut.charging = false;
            _UsbCharger.Connected = true;
            _door.isDoorClosed = true;
            
            _RFID.RfidChangedEvent += Raise.EventWith(this, new RFIDEventArgs(){Id = 5});
            Assert.That(_uut.oldID, Is.EqualTo(5));
        }

        [Test]
        public void TestGetChargingstationState()
        {
            
            _uut.state = StationControl.ChargingStationState.Available;
            Assert.That(_uut.state, Is.EqualTo(StationControl.ChargingStationState.Available));
        }

        [Test]
        public void SetDatetime()
        {
            DateTime testtime = new DateTime(2020, 12, 24, 18, 30, 0);
            _uut.TimeStamp = testtime;
            Assert.That(_uut.TimeStamp, Is.EqualTo(testtime));
        }
      

    }
}
