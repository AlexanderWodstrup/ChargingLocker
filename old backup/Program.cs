using System;
using System.Runtime.CompilerServices;
using ChargingLocker;
using RFIDSimulator;

namespace ConsoleAppProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // Assemble your system here from all the classes
            StationControl stationControl = new StationControl();
            Door door = new Door();
            IRFIDReader rfidReader = new RFIDReaderSimulator();
            LogWriter logWriter = new LogWriter();

            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.DoorOpened();
                        break;

                    case 'C':
                        door.DoorClosed();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.RFIDValueEvent += logWriter.LogDoorLocked;
                        rfidReader.ReadRFID(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
