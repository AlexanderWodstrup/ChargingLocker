using System;
using ChargingLocker.ClassLibrary.Interfaces;

namespace ChargingLocker.ClassLibrary
{
    public class StationControl : IStationControl
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
        private IRFIDReader _rfidReader;
        private IChargeControl _charger;
        private IDoor _door = new Door();
        private IDisplay _display;
        private ILogWriter _log;
        private int _oldId;
        private int _id;
        public  StationControl(IDoor door, IUsbCharger _usbCharger, IDisplay display,IRFIDReader rfidReader, ILogWriter log, IChargeControl charge)
        {
            _log = log;
            _door = door;
            _display = display;
            _rfidReader = rfidReader;
            _rfidReader.RFIDValueEvent += RfidDetected;
            door.DoorValueEvent += DisplayDoor;
            _state = LadeskabState.Available;
            _charger = charge;
        }

        public void runProgram(int id)
        {
            _rfidReader.ReadRFID(id);
        }

        public string GetState()
        {
            string tmp = _state.ToString();
            return tmp;
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

        public void RfidDetected(object sender, RFIDEventArgs e)
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
                            Console.WriteLine("DEBUG:::_state after = " + _state);
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
