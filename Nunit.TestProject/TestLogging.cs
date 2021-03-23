﻿using System;
using System.Collections.Generic;
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


    }
}
