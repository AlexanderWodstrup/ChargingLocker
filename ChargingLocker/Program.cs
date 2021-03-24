﻿using System;
using System.Runtime.CompilerServices;
using ChargingLocker;
using Microsoft.VisualStudio.TestPlatform.Common;
using RFIDSimulator;

namespace ChargingLocker
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            Door door = new Door();
            StationControl stationControl = new StationControl(door);
            IRFIDReader rfidReader = new RFIDReaderSimulator();
            LogWriter logWriter = new LogWriter();           

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
                        door.DoorOpened();
                        break;

                    case "Close" or "close":
                        door.DoorClosed();
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
