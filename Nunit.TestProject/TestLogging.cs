using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ClassLibrary.Logging;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Nunit.TestProject
{
    public class TestLogging
    {
        private Logging _uut;
        private string logFileName = "logfile.txt";
        [SetUp]
        public void Setup()
        {
           
            // log = Substitute.For<Logging>();
             //file = Substitute.For<ILogFile>();
              _uut= new Logging();
        }

       
        [Test]
        public void CanLog()
        {
            Assert.DoesNotThrow(()=>_uut.Log(DateTime.Today, 1, "test"));
        }

        [Test]
        public void FindFile()
        {
            
            
            _uut.Log(DateTime.Today, 1, "Test");
            Assert.That(File.Exists(logFileName));
        }

        [Test]
        public void WriteToFile()
        {

            if (File.Exists(logFileName))
            {
                File.Delete(logFileName);
            }

            _uut.Log(DateTime.Today, 1, "test");

            DateTime time = DateTime.Today;
            string text;
            using (StreamReader reader = new StreamReader(File.OpenRead(logFileName)))
            {
                text = reader.ReadLine();
            }
            Assert.That(text == DateTime.Today.ToString() +": "+ "1: test");
        }

        [Test]
        public void WriteToFile_SeveralLine()
        {

            if (File.Exists(logFileName))
            {
                File.Delete(logFileName);
            }

            _uut.Log(DateTime.Today, 1, "test");
            _uut.Log(DateTime.Today, 2, "test2");
            _uut.Log(DateTime.Today, 3, "test3");

            DateTime time = DateTime.Today;
            string text;
            using (StreamReader reader = new StreamReader(File.OpenRead(logFileName)))
            {
                text = reader.ReadToEnd();
            }
            Assert.That(text == DateTime.Today.ToString() + ": " + "1: test"+ "\r\n" + DateTime.Today.ToString() + ": " + "2: test2"+ "\r\n" + DateTime.Today.ToString() + ": " + "3: test3\r\n");

        }

        [Test]
        public void CanItRunMoreThanOnce()
        {
            ILogging uut = Substitute.For<ILogging>();
            var counter = 0;

            
            uut.When(x => x.Log(DateTime.Today, 1, "Test")).Do(x => counter++);
            

            uut.Log(DateTime.Today, 1, "Test");
            uut.Log(DateTime.Today, 1, "Test");
            
            Assert.AreEqual(2, counter);
        }

    }
}
