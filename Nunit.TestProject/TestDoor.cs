using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;
using ClassLibrary.DoorObserver;
using NUnit.Framework;

namespace Nunit.TestProject
{
    public class TestDoor
    {
        private IDoor _uut;
        [SetUp]
        public void Setup()
        { 
            _uut = new Door();

        }


        //EKsempel på dørtest
        [Test]
        public void CloseDoor()
        {
            _uut.isDoorClosed = false;
            _uut.CloseDoor();
            Assert.That(_uut.isDoorClosed, Is.EqualTo(true));
        }

        [Test]
        public void OpenDoor()
        {
            _uut.isDoorClosed = true;
            _uut.OpenDoor();
            Assert.That(_uut.isDoorClosed, Is.EqualTo(false) );
        }

        [Test]
        public void getDoor()
        {
            _uut.IsDoorLocked = false;
            Assert.That(_uut.IsDoorLocked, Is.EqualTo(false));
        }
    }
}
