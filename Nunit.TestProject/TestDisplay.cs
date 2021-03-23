using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary;
using ClassLibrary.Display;
using NUnit.Framework;

namespace Nunit.TestProject
{
    public class TestDisplay
    {
        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [Test]
        public void PrintMessage()
        {
            string testString = "Message";
            _uut.PrintMessage(testString);

        }
    }
}
