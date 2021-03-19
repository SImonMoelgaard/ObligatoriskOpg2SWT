using ClassLibrary;
using ClassLibrary.RFIDObserver;
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
            Assert.That(_uut.CardID == 88888888);
            
        }
    }
}