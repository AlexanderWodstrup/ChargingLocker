using System;

namespace ChargingLocker.ClassLibrary
{
    public class Display : IDisplay
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

        public void DisplayDoorOpen()
        {
            Console.WriteLine("Please close the door before scaning your RFID tag");
        }

        public void DisplayPhoneCharging(double current)
        {
            Console.WriteLine("Your device is now charging: {0}%",current);
        }

        public void DisplayPhoneFullyCharged()
        {
            Console.WriteLine("Your device is now fully charged");
        }
    }
}
