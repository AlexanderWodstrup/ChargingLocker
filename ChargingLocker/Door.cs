using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker
{
    public class Door
    {
        private bool _doorOpen = false;
        private bool _doorClose = true;
        private bool _lock = false;

        public void DoorOpened()
        {
            _doorClose = false;
            _doorOpen = true;
        }

        public void DoorClosed()
        {
            _doorOpen = false;
            _doorClose = true;
        }

        public void LockDoor()
        {
            _lock = true;
        }

        public void UnlockDoor()
        {
            _lock = false;
        }
    }
}
