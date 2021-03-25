using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker.ClassLibrary
{
    public interface IDisplay
    {
        public void DisplayConnectPhone();


        public void DisplayScanRFID();


        public void DisplayFailedConnection();


        public void DisplayChargeLockerOccupied();


        public void DisplayWrongRFID();


        public void DisplayRemovePhone();


        public void DisplayDoorOpen();

        public void DisplayPhoneCharging(double current);

        public void DisplayPhoneFullyCharged();

    }
}
