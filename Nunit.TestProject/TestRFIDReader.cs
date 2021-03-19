using ClassLibrary;
using ClassLibrary.RFIDObserver;
using NSubstitute;
using NUnit.Framework;

namespace Nunit.TestProject
{
    public class Tests
    {
        private IRFIDReader _uut;
        private RFIDEventArgs RFIDEvent;
        [SetUp]
        public void Setup()
        { 
            _uut = new RfidReader();
            RFIDEvent = null;
            _uut.RfidChangedEvent += (sender, args) => { RFIDEvent = args;};

        }

        [Test]
        public void RFIDSet()
        {
            _uut.CardID = 10;
            Assert.That(_uut.CardID, Is.EqualTo(10));


        }

        [Test]
        public void NewRFIDSet()
        {
            _uut.CardID = 10;
            

            Assert.That(RFIDEvent, Is.EqualTo(10));
        }

        [Test]
        public void RFIDEventLower()
        {
           // int lastRFID = 0;


           _uut.RfidChangedEvent += (sender, args) => args.Id;
           Assert.That(_uut.CardID, Is.Zero);
        }
    }
}