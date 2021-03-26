using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using ChargingLocker.ClassLibrary;
using ChargingLocker.ClassLibrary.Interfaces;
using NSubstitute.ReceivedExtensions;

namespace ChargingLocker.Test.Unit
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IUsbCharger _usbCharger;
        private IDisplay _display;
        private IRFIDReader _rfidReader;
        private RFIDEventArgs _rfidEventArgs;
        private IChargeControl _chargeControl;
        private ILogWriter _log;

        [SetUp]
        public void Setup()
        {
            _rfidEventArgs = null;
            _door = Substitute.For<IDoor>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _chargeControl = Substitute.For<IChargeControl>();
            _log = Substitute.For<ILogWriter>();
            _rfidReader = Substitute.For<IRFIDReader>(); //ASK why i cant use interface version
            
            
            _uut = new StationControl(_door, _display, _rfidReader, _log,_chargeControl);
            _rfidReader.RFIDValueEvent += (o, args) =>
            {
                _rfidEventArgs = args;
            };
        }

        [Test]
        public void OpenEventTrue()
        {
            
            _door.DoorValueEvent += Raise.EventWith(new DoorEventArgs { _doorOpen = true });
            _display.Received().DisplayConnectPhone();

        }
        [Test]
        public void DoorEventFalse()
        {
            
            _door.DoorValueEvent += Raise.EventWith(new DoorEventArgs { _doorOpen = false });
            _display.Received().DisplayScanRFID();

        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RunProgram_RFIDReader_ReceivedCorrectId(int id)
        {
            _uut.runProgram(id);
            _rfidReader.Received().ReadRFID(id);
        }


        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_DoorStatusTrue_Display_DisplayOpenDoor(int TestId)
        {
            _door.CurrentDoorStatus.Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs {id=TestId});
            
            _display.Received().DisplayDoorOpen();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_DoorStatusFalse_ChargeIsConnectedTrue_DoorReceived_LockDoor(int TestId)
        {
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _door.Received().LockDoor();

        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_DoorStatusFalse_ChargeIsConnectedTrue_ChargeControlReceived_StartCharge(int TestId)
        {
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _chargeControl.Received().StartCharge();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_DoorStatusFalse_ChargeIsConnectedTrue_LogReceived_LogDoorLocked(int TestId)
        {
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _log.Received().LogDoorLocked(TestId);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_DoorStatusFalse_ChargeIsConnectedTrue_DisplayReceived_DisplayChargeLockerOccupied(int TestId)
        {
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _display.Received().DisplayChargeLockerOccupied();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_DoorStatusFalse_ChargeIsConnectedTrue_StateEqualTo_LadeskabStateLocked(int TestId)
        {
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            Assert.That(_uut.GetState(),Is.EqualTo("Locked"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_DoorStatusFalse_ChargeIsConnectedFalse_DisplayReceived_DisplayFailedConnection(int TestId)
        {
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(false);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _display.Received().DisplayFailedConnection();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_StateLocked_With_Correct_ID_ChargeControlReceived_StopCharge(int TestId)
        {
            //This is to lock the door with testId so we can test rfidDetected when state is locked
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });


            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _chargeControl.Received().StopCharge();
            
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_StateLocked_With_Correct_ID_DoorReceived_UnlockDoor(int TestId)
        {
            //This is to lock the door with testId so we can test rfidDetected when state is locked
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });


            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _door.Received().UnlockDoor();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_StateLocked_With_Correct_ID_LogReceived_LogDoorUnlocked(int TestId)
        {
            //This is to lock the door with testId so we can test rfidDetected when state is locked
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });


            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _log.Received().LogDoorUnlocked(TestId);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_StateLocked_With_Correct_ID_DisplayReceived_DisplayRemovePhone(int TestId)
        {
            //This is to lock the door with testId so we can test rfidDetected when state is locked
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });


            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _display.Received().DisplayRemovePhone();
            
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_UnlockLocker_With_Wrong_ID(int TestId)
        {
            //This is to lock the door with testId so we can test rfidDetected when state is locked
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });


            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId+1 });
            _display.Received().DisplayWrongRFID();
            _log.Received().LogDoorTriedUnlockedWithWrongId(TestId+1);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(25)]
        public void RfidDetected_StateLocked_With_Correct_ID_State_IsEqualTo_Available(int TestId)
        {
            //This is to lock the door with testId so we can test rfidDetected when state is locked
            _door.CurrentDoorStatus.Returns(false);
            _chargeControl.isConnected().Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });


            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            Assert.That(_uut.GetState(),Is.EqualTo("Available"));
        }

    }
}
