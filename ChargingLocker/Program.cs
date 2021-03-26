using System;
using System.Runtime.CompilerServices;
using ChargingLocker;
using ChargingLocker.ClassLibrary;
using Microsoft.VisualStudio.TestPlatform.Common;

namespace ChargingLocker
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            IDoor _door = new Door();
            IUsbCharger _usbCharger = new UsbChargerSimulator();
            IDisplay _display = new Display();
            IRFIDReader _rfidReader = new RFIDReaderSimulator();
            IStationControl stationControl = new StationControl(_door, _usbCharger, _display, _rfidReader);

            int runs = 0;
            bool finish = false;
            do
            {
                string input;
                
                if (runs == 0)
                {
                    System.Console.WriteLine("Welcome to ChargingLocker");
                    System.Console.WriteLine("Type 'Open' to open locker door");
                    System.Console.WriteLine("Type 'Close' to close locker door");
                    System.Console.WriteLine("Type 'Scan' to scan your RFID tag");
                    System.Console.WriteLine("Type 'Exit' to exit program");
                }
                else
                {
                    System.Console.WriteLine("Type 'List' to show all options");
                }
                runs = 1;
                System.Console.Write("Input: ");
                input = Console.ReadLine();

                if(string.IsNullOrEmpty(input)) continue;
                switch (input)
                {
                    case "Exit" or "exit":
                        finish = true;
                        break;

                    case "Open" or "open":
                        _door.DoorOpened();
                        
                        break;

                    case "Close" or "close":
                        _door.DoorClosed();
                        
                        break;

                    case "Scan" or "scan":
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        stationControl.runProgram(id);
                        break;
                    case "List" or "list":
                        runs = 0;
                        break;
                    default:
                        break;
                }

                
            } while (!finish);
        }
    }
}
