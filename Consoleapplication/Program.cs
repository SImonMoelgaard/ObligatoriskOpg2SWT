    using System;
    using ClassLibrary;
    using ClassLibrary.DoorObserver;
    using ClassLibrary.RFIDObserver;

    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            IDoor door = new Door();
            IRFIDReader rfidReader = new RfidReader();


            var cont = true;

            Console.WriteLine("E --- Exit");
            Console.WriteLine("O --- Open Door");
            Console.WriteLine("C --- Close Door");
            Console.WriteLine("R --- Read RFID");


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
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.RFIDDetected(id);
                        break;

                }
            }



        }
    }


