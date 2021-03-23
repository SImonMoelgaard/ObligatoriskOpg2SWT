using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ClassLibrary.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Nunit.TestProject
{
    public class TestLogging
    {
        private ILogging _uut;
        [SetUp]
        public void Setup()
        { 
            _uut = new Logging();
        }

        [Test]
        public void Log()
        {

            var counter = 0;
            var log = Substitute.For<ILogging>();
            
            log.When(x => x.Log(DateTime.Today, 1, "2")).Do(x => counter++);
            //string testString = "Message";

            log.Log(DateTime.Today, 1, "2");
            log.Log(DateTime.Today, 1, "2");
            
            Assert.AreEqual(2, counter);
        }

        [Test]
        public void LogTest()
        {
            var log = Substitute.For<ILogging>();
            log.Log(DateTime.Now, 1, "2");

            log.ReceivedWithAnyArgs(1).Log(default, default, default);
            //_uut.Log(DateTime.Now, 1, "2");
            //Assert.Pass();


        }
    }
}
