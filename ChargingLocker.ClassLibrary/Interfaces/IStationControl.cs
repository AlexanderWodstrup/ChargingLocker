using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker.ClassLibrary
{
    public interface IStationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        void runProgram(int id);

        void DisplayDoor(object sender, DoorEventArgs e);

        void RfidDetected(object sender, RFIDEventArgs e);

    }
}

