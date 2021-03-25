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

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new StationControl(_door,_usbCharger,_display);
        }

        //[Test]
        //public void OpenDoor()
        //{
        //    _door.DoorOpened();

        //    _door.CurrentDoorStatus.Returns(true);

        //}
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
    }

}
