using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.UsbObserver;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;

namespace Nunit.TestProject
{
    class TestChargeControl
    {
        private IChargeControl _uut;
        private IUsbCharger _usbCharger;


        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_usbCharger);
        }

        [Test]
        public void InvalidChargeHandleEventWithMinusOne()
        {
            Assert.Throws<InvalidOperationException>(()=>_usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = -1 }));
        }

        [Test]
        public void InvalidChargeHandleEventWith501()
        {
            Assert.Throws<InvalidOperationException>(()=>_usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 501}));
        }

        [Test]
        public void ChargeHandleEventWith1()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 1});
            _usbCharger.Received(1).StopCharge();
        }

        [Test]
        public void ChargeHandleEventWith5()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 5});
            _usbCharger.Received(1).StopCharge();
        }

        [Test]
        public void ChargeHandleEventWith6()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() {Current = 6});
            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void ChargeHandleEventWith500()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 500 });
            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void StartCharge()
        {
            _usbCharger.StartCharge();
            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void StopCharge()
        {
            _usbCharger.StopCharge();
            _usbCharger.Received(1).StopCharge();
        }

        [Test]
        public void GetWatt()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 50 });
            Assert.That(_uut.Watt, Is.EqualTo(50));
        }

        [Test]
        public void WattSet()
        {
            _uut.Watt = 20;
            Assert.That(_uut.Watt, Is.EqualTo(20));
        }
    }
}
