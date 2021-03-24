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
        private IUsbCharger _charger = new UsbChargerSimulator();
        LogWriter logWriter = new LogWriter();
        private Door _door = new Door();
        private Display _display = new Display();
        private LogWriter _log = new LogWriter();
        private int _oldId;
        public event EventHandler<RFIDEventArgs> RFIDValueEvent;

        public  StationControl()
        {
            rfidReader.RFIDValueEvent += sendID;
        }

        public void runProgram(int id)
        {
            _state = LadeskabState.Available;
            rfidReader.ReadRFID(id);
        }
        // Her mangler constructor
        public void sendID(object sender, RFIDEventArgs e)
        {
            //Console.WriteLine("SendID Called");
            int id = e.id;
            RfidDetected(id);
            //logWriter.LogDoorLocked(id);
            
        }
        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        _log.LogDoorLocked(id);
                        

                        _display.DisplayChargeLockerOccupied();
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.DisplayFailedConnection();
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    _display.DisplayScanRFID();
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();

                        _display.DisplayRemovePhone();
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.DisplayWrongRFID();
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
