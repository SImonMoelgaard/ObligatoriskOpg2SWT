    using System;
    using System.Runtime.CompilerServices;
    using ClassLibrary;
    using ClassLibrary.Display;
    using ClassLibrary.DoorObserver;
    using ClassLibrary.Logging;
    using ClassLibrary.RFIDObserver;
    using ClassLibrary.UsbObserver;
    using Ladeskab;
    using NSubstitute.Core.Arguments;
    using UsbSimulator;

    class Program
    {
        static void Main(string[] args)
        {

            
            // Assemble your system here from all the classes
            IDoor door = new Door();
            IRFIDReader rfidReader = new RfidReader();
            IUsbCharger usbcharger = new UsbChargerSimulator();
            ChargeControl chargeControl = new ChargeControl(usbcharger);
            IDisplay display = new Display();
            
            ILogging Log = new Logging();
            

            StationControl stationcontroller = new StationControl(display, door, rfidReader, usbcharger, chargeControl, Log);


            var cont = true;

            Console.WriteLine("E --- Exit");
            Console.WriteLine("O --- Open Door");
            Console.WriteLine("C --- Close Door");
            Console.WriteLine("R --- Read RFID");
            Console.WriteLine("T --- Tilslut telefon");


            while (cont)
            {
                var key = Console.ReadKey(true);
                switch (key.KeyChar)
                {
                    case 'e':
                    case 'E':
                        cont = false;
                        break;
                    case 'O':
                    case 'o':
                        door.OpenDoor();
                        break;

                    case 'C':
                    case 'c':
                        door.CloseDoor();
                        break;

                    case 'R':
                    case 'r':
                        rfidReader.CardID = 88888888;
                        
                        
                        //System.Console.WriteLine("Indtast RFID id: ");
                        //string idString = System.Console.ReadLine();

                        //int id = Convert.ToInt32(idString);
                        //rfidReader.RFIDDetected(id);
                        break;
                case 'T':
                case 't':
                    usbcharger.Connected = true;
                    Console.WriteLine("(Handling) Telefon tilsluttes");
                    break;

                }
            }



        }
    }


