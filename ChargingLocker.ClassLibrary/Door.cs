using System;

namespace ChargingLocker.ClassLibrary
{
    public class Door : IDoor
    {
        private bool _lock {get; set; }
        public bool CurrentDoorStatus { get; private set; }
        public event EventHandler<DoorEventArgs> DoorValueEvent;
        
        public void DoorOpened()
        {
            
            if (_lock == true)
            {
                Console.WriteLine("The door is locked, please scan your RFID tag");
            }
            else
            {
                CurrentDoorStatus = true;

#if DEBUG
                Console.WriteLine("DEBUG:::Door opened");
#endif
                DoorValueEvent?.Invoke(this, new DoorEventArgs() { _doorOpen = true });
            }
            
        }
        
        public void DoorClosed()
        {
            if (_lock == true)
            {
                Console.WriteLine("The door is closed and locked, please scan your RFID tag");
            }
            else
            {
                CurrentDoorStatus = false;
#if DEBUG
                Console.WriteLine("DEBUG:::Door closed");
#endif
                DoorValueEvent?.Invoke(this, new DoorEventArgs() {_doorOpen = false});
            }
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
