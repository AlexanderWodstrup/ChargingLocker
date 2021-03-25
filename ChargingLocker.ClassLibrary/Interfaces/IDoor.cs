using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker.ClassLibrary
{
    public class DoorEventArgs : EventArgs
    {
        public bool _doorOpen { get; set; }
    }

    public interface IDoor
    {

        bool CurrentDoorStatus { get; }
        event EventHandler<DoorEventArgs> DoorValueEvent;

        void DoorOpened();

        void DoorClosed();

        void LockDoor();


        void UnlockDoor();
    }
}
