using System;
using System.Runtime.CompilerServices;
using ChargingLocker;

namespace ChargingLocker
{
    class Program
    {
        static private Door _door;
        static private IRFIDReader _rfidReader;
        static void Main(string[] args)
        {
				// Assemble your system here from all the classes
                

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
                        _door.DoorOpened();
                        break;

                    case 'C':
                        _door.DoorClosed();
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        _rfidReader.ReadRFID(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
