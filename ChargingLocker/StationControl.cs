using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker;
using RFIDSimulator;
using UsbSimulator;

namespace ChargingLocker
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
        public event EventHandler<RFIDEventArgs> RFIDValueEvent;
        private int id;
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
            id = e.id;
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
                            _oldId = id;
                            _log.LogDoorLocked(id);

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
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _log.LogDoorUnlocked(id);
                        _display.DisplayRemovePhone();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayWrongRFID();
                        _log.LogDoorTriedUnlockedWithWrongId(id);
                    }

                    break;
            }
        }
    }
}
