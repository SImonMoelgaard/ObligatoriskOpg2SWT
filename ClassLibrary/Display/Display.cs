using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClassLibrary.UsbObserver;

namespace ClassLibrary.Display
{
    public class Display : IDisplay
    {
         

        public void PrintConnectPhone()
        {
            Console.WriteLine(("Tilslut telefon"));
        }

        public void PrintLoadRFID()
        {
            Console.WriteLine("Indlæs RFID");
        }

        public void PrintConnectionFailure()
        {
            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        public void PrintCharging()
        {
            Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void PrintRFIDFail()
        {
            Console.WriteLine("RFID fejl");
        }

        public void PrintRemovePhone()
        {
            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
        }
    }
    
}
