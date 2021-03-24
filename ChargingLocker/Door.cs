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
        public bool _doorOpen { get; set; }
        public void DoorOpened()
        {
            _doorOpen = true;
#if DEBUG
            Console.WriteLine("DEBUG:::Door opened");
#endif

        }
        
        public void DoorClosed()
        {
            _doorOpen = false;
#if DEBUG
            Console.WriteLine("DEBUG:::Door closed");
#endif
        }

        public void LockDoor()
        {
            _lock = true;
#if DEBUG
            Console.WriteLine("DEBUG:::Door locked");
#endif
        }

        public void UnlockDoor()
        {
            _lock = false;
#if DEBUG
            Console.WriteLine("DEBUG:::Door unlocked");
#endif
        }
    }
}
