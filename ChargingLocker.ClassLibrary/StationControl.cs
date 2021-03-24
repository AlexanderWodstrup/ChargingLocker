using System;

namespace ChargingLocker.ClassLibrary
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        IRFIDReader rfidReader = new RFIDReaderSimulator();
        private ChargeControl _charger = new ChargeControl();
        private Door _door;
        private Display _display = new Display();
        private LogWriter _log = new LogWriter();
        private int _oldId;
        private int _id;
        public  StationControl(Door door)
        {
            _door = door;
            rfidReader.RFIDValueEvent += RfidDetected;
            door.DoorValueEvent += DisplayDoor;
            _state = LadeskabState.Available;
        }

        public void runProgram(int id)
        {
            rfidReader.ReadRFID(id);
        }

        public void DisplayDoor(object sender, DoorEventArgs e)
        {
            if (e._doorOpen == true)
            {
                _display.DisplayConnectPhone();
            }

            if (e._doorOpen == false)
            {
                _display.DisplayScanRFID();
            }
        }

        private void RfidDetected(object sender, RFIDEventArgs e)
        {
            _id = e.id;
            switch (_state)
            {
                case LadeskabState.Available:
                    
#if DEBUG
                    Console.WriteLine("DEBUG:::_state = LadeskabState.Available");
#endif
                    if (_door.CurrentDoorStatus == true)
                    {
                        _display.DisplayDoorOpen();
                    }
                    else
                    {
                        // Check for ladeforbindelse
                        if (_charger.isConnected())
                        {
                            _door.LockDoor();
                            _charger.StartCharge();
                            _oldId = _id;
                            _log.LogDoorLocked(_id);

                            _display.DisplayChargeLockerOccupied();
                            _state = LadeskabState.Locked;
#if DEBUG
                            Console.WriteLine("DEBUG:::_state = " + _state);
#endif
                        }
                        else
                        {
                            _display.DisplayFailedConnection();
                        }
                    }
                    

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
#if DEBUG
                    Console.WriteLine("DEBUG:::_state = LadeskabState.Locked");
#endif
                    if (_id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _log.LogDoorUnlocked(_id);
                        _display.DisplayRemovePhone();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayWrongRFID();
                        _log.LogDoorTriedUnlockedWithWrongId(_id);
                    }

                    break;
            }
        }
    }
}
