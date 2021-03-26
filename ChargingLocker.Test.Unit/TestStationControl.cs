using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using ChargingLocker.ClassLibrary;
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

        [SetUp]
        public void Setup()
        {
            _rfidEventArgs = null;
            _door = Substitute.For<IDoor>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _chargeControl = Substitute.For<IChargeControl>();
            _rfidReader = Substitute.For<RFIDReaderSimulator>(); //ASK why i cant use interface version
            
            _rfidReader.RFIDValueEvent += (o, args) =>
            {
                _rfidEventArgs = args;
            };
            _uut = new StationControl(_door, _usbCharger, _display, _rfidReader);
            
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

        [TestCase(25)]
        public void Test_Of_RunProgram_EventFired(int id)
        {
            _uut.runProgram(id);
            //_rfidReader.Received().ReadRFID(id);
            Assert.That(_rfidEventArgs, Is.Not.Null);
        }

        [TestCase(25)]
        public void Test_Of_RunProgram_CorrectEventReceived(int id)
        {
            _uut.runProgram(id);
            Assert.That(_rfidEventArgs.id,Is.EqualTo(id));
        }

        [TestCase(25)]
        public void Test_Of_RfidDetected_Display_DisplayOpenDoor(int TestId)
        {
            _door.CurrentDoorStatus.Returns(true);
            _rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs {id=TestId});
            _door.CurrentDoorStatus.Returns(true);
            _display.Received().DisplayDoorOpen();
        }

        [TestCase(25)]
        public void Test_Of_RfidDetected(int TestId)
        {
            _door.CurrentDoorStatus.Returns(true);
            _chargeControl.isConnected().Returns(true);
            //_rfidReader.RFIDValueEvent += Raise.EventWith(new RFIDEventArgs { id = TestId });
            _door.CurrentDoorStatus.Returns(true);
            _chargeControl.isConnected().Returns(true);
            _display.Received().DisplayDoorOpen();
        }
    }
}
