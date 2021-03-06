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
        public void RFIDSet_FalseValue_11()
        {
            _uut.CardID = 10;
            Assert.That(_uut.CardID, Is.Not.EqualTo(11));
        }
        [Test]
        public void RFIDSet_FalseValue_9()
        {
            _uut.CardID = 10;
            Assert.That(_uut.CardID, Is.Not.EqualTo(9));
        }

        [Test]
        public void RFIDGet()
        {
            _uut.CardID = 10;
            Assert.That(RFIDEvent.Id, Is.EqualTo(10));
        }

        [Test]
        public void RFIDGet_FalseValue_11()
        {
            _uut.CardID = 10;
            Assert.That(RFIDEvent.Id, Is.Not.EqualTo(11));
        }

        [Test]
        public void RFIDGet_FalseValue_9()
        {
            _uut.CardID = 10;
            Assert.That(RFIDEvent.Id, Is.Not.EqualTo(11));
        }

        [Test]
        public void RFIDEventLower()
        {
           // int lastRFID = 0;


          
           Assert.That(_uut.CardID, Is.Zero);
        }
    }
}