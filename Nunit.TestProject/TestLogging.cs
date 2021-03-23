using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary.Logging;
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
            
        }


    }
}
