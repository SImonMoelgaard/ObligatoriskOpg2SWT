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
        private ILogging log;
        [SetUp]
        public void Setup()
        { 
             log = Substitute.For<ILogging>();
        }

        [Test]
        public void Log()
        {

            var counter = 0;
            
            
            log.When(x => x.Log(DateTime.Today, 1, "2")).Do(x => counter++);
            //string testString = "Message";

            log.Log(DateTime.Today, 1, "2");
            log.Log(DateTime.Today, 1, "2");
            
            Assert.AreEqual(2, counter);
        }

        [Test]
        public void LogTest()
        {
            log.Log(DateTime.Now, 1, "2");
            log.Received(3);
            //_uut.Log(DateTime.Now, 1, "2");
            //Assert.Pass();


        }
    }
}
