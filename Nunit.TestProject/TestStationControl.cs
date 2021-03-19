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
            _uut.state = StationControl.ChargingStationState.Opened;
            _UsbCharger.Connected = false;
            _door.DoorChangedEvent += Raise.EventWith(this, new DoorStatusEventArgs()
            {
                IsClosed = true
            });

            _display.Received(0).PrintMessage("Tilslut telefon");
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

            _display.Received(0).PrintMessage("Dør åbnet.Tilslut venligst telefonen");
        }


        [Test]
        public void WattSet()
        {
            _uut.Watt = 20;
            Assert.That(_uut.Watt, Is.EqualTo(20));
        }

        [Test]
        public void WattGet()
        {
            
        }
    }
}
