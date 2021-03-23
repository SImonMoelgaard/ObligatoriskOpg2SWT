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
        public void LogUnlock()
        {

            var counter = 0;
            
            
            log.When(x => x.LogUnlocked(DateTime.Today, 1)).Do(x => counter++);
            //string testString = "Message";

            log.LogUnlocked(DateTime.Today, 1);
            log.LogUnlocked(DateTime.Today, 1);
            
            Assert.AreEqual(2, counter);
        }

        [Test]
        public void LogTest()
        {
            log.LogUnlocked(DateTime.Now, 1);
            log.Received(1);
            //_uut.Log(DateTime.Now, 1, "2");
            //Assert.Pass();
            

        }
    }
}
