using System;

namespace ChargingLocker.ClassLibrary
{
    public class Door : IDoor
    {
        public bool _lock {get; private set; }
        public bool CurrentDoorStatus { get; private set; }
        public event EventHandler<DoorEventArgs> DoorValueEvent;
        
        public void DoorOpened()
        {
            
            
            CurrentDoorStatus = true;

#if DEBUG
            Console.WriteLine("DEBUG:::Door opened");
#endif
            DoorValueEvent?.Invoke(this, new DoorEventArgs() { _doorOpen = true });

        }
        
        public void DoorClosed()
        {
            
            CurrentDoorStatus = false;
#if DEBUG
            Console.WriteLine("DEBUG:::Door closed");
#endif
            DoorValueEvent?.Invoke(this, new DoorEventArgs() {_doorOpen = false});
            
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
