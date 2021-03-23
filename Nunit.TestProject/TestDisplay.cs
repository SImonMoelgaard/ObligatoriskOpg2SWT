using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClassLibrary;
using ClassLibrary.Display;
using NSubstitute;
using NUnit.Framework;

namespace Nunit.TestProject
{
    public class TestDisplay
    {
        private IDisplay _uut;
        private StringWriter readConsole;
        [SetUp]
        public void Setup()
        {
            _uut = new Display();
            readConsole = new StringWriter();
            System.Console.SetOut(readConsole);

        }


        [Test]
        public void ConsolePrint()
        {
            _uut.PrintMessage("Test");
            var text = readConsole.ToString();
            Assert.AreEqual("Test\r\n",text);

        }

        [Test]
        public void PrintMessage()
        {
            var counter = 0;
            _uut = Substitute.For<IDisplay>();
            
            _uut.When(x => x.PrintMessage("Test")).Do(x => counter++);
           

            _uut.PrintMessage("Test");
            _uut.PrintMessage("Test");
            Assert.AreEqual(2, counter);
        }

       
    }
}
