using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Channels;
using ClassLibrary.UsbObserver;

namespace ClassLibrary.Display
{
    public class Display : IDisplay
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
    
}
