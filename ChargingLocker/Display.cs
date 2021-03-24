using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker
{
    public class Display
    {
        public void DisplayConnectPhone()
        {
            Console.WriteLine("Please connect your phone");
        }

        public void DisplayScanRFID()
        {
            Console.WriteLine("Please scan your tag");
        }

        public void DisplayFailedConnection()
        {
            Console.WriteLine("The system could not connect to your device, please try again");
        }

        public void DisplayChargeLockerOccupied()
        {
            Console.WriteLine("This locker is now occupied by you");
        }

        public void DisplayWrongRFID()
        {
            Console.WriteLine("Your tag could not be found, please try again");
        }

        public void DisplayRemovePhone()
        {
            Console.WriteLine("Please remove your device");
        }
    }
}
