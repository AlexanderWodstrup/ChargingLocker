using System;

namespace ChargingLocker.ClassLibrary
{
    public class Display : IDisplay
    {
        public string OutputString { get; private set; }
        public void DisplayConnectPhone()
        {
            OutputString = "Please connect your phone";
            Console.WriteLine(OutputString);

        }

        public void DisplayScanRFID()
        {
            OutputString = "Please scan your tag";
            Console.WriteLine(OutputString);
        }

        public void DisplayFailedConnection()
        {
            OutputString = "The system could not connect to your device, please try again";
            Console.WriteLine(OutputString);
        }

        public void DisplayChargeLockerOccupied()
        {
            OutputString = "This locker is now occupied by you";
            Console.WriteLine(OutputString);
        }

        public void DisplayWrongRFID()
        {
            OutputString = "Your tag could not be found, please try again";
            Console.WriteLine(OutputString);
        }

        public void DisplayRemovePhone()
        {
            OutputString = "Please remove your device";
            Console.WriteLine(OutputString);
        }

        public void DisplayDoorOpen()
        {
            OutputString = "Please close the door before scaning your RFID tag";
            Console.WriteLine(OutputString);
        }

        public void DisplayPhoneCharging(double current)
        {
            OutputString = "Your device is now charging: " + current + "%";
            Console.WriteLine(OutputString);
        }

        public void DisplayPhoneFullyCharged()
        {
            OutputString = "Your device is now fully charged";
            Console.WriteLine(OutputString);
        }
    }
}
