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
        public void GetDoor()
        {
            _uut.IsDoorLocked = false;
            Assert.That(_uut.IsDoorLocked, Is.EqualTo(false));
        }

        [Test]
        public void LockDoor()
        {
            _uut.IsDoorLocked = false;
            _uut.isDoorClosed = true;
            _uut.LockDoor();
            Assert.That(_uut.IsDoorLocked, Is.EqualTo(true));
        }
        [Test]
        public void UnlockDoor()
        {
            _uut.IsDoorLocked = true;
            _uut.UnlockDoor();
            Assert.That(_uut.IsDoorLocked, Is.EqualTo(false));
        }
        
        

    }
}
