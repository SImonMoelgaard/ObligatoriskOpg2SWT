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
            Assert.That(_usbCharger.CurrentValue, Is.EqualTo(0));
        }

        [Test]
        public void ChargeHandleEventWith5()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 5});
            Assert.That(_usbCharger.CurrentValue, Is.EqualTo(0));
        }

        [Test]
        public void ChargeHandleEventWith6()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() {Current = 100});
            Assert.That(_usbCharger.CurrentValue, Is.GreaterThan(500));
        }
    }
}
