using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker
{
    class Door
    {
        private bool _doorOpen = false;
        private bool _doorClose = true;
        private bool _lock = false;

        void DoorOpened()
        {
            _doorClose = false;
            _doorOpen = true;
        }

        void DoorClosed()
        {
            _doorOpen = false;
            _doorClose = true;
        }

        void LockDoor()
        {
            _lock = true;
        }

        void UnlockDoor()
        {
            _lock = false;
        }
    }
}
