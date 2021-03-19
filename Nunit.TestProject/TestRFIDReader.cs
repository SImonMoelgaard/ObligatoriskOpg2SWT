using ClassLibrary;
using ClassLibrary.RFIDObserver;
using NSubstitute;
using NUnit.Framework;

namespace Nunit.TestProject
{
    public class Tests
    {
        private IRFIDReader _uut;
        [SetUp]
        public void Setup()
        {
            
            _uut = new RfidReader();
        }

        [Test]
        public void CtorRFID()
        {
            _uut.CardID = 10;
            Assert.That(_uut.CardID == 10);


        }

        [Test]
        public void RFIDEvent()
        {
            double lastRFID = 88888888;
            _uut.RfidChangedEvent += (sender, args) => lastRFID = args.Id;


        }
    }
}