using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker
{
    public class Door
    {
        private bool _lock {get; set; }
        private bool _doorOpen { get; set; }
        public void DoorOpened()
        {
            _doorOpen = true;
        }
        
        public void DoorClosed()
        {
            _doorOpen = false;
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
