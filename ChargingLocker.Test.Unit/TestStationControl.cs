using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using ChargingLocker.ClassLibrary;

namespace ChargingLocker.Test.Unit
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new StationControl(_door,_usbCharger);
        }

        //[Test]
        //public void OpenDoor()
        //{
        //    _door.DoorOpened();

        //    _door.CurrentDoorStatus.Returns(true);

        //}
        [Test]
        public void OpenDoorEvent()
        {
            bool testValue;
            _door.DoorOpened();
            _door.DoorValueEvent += (o, args) =>
            {
                testValue = args._doorOpen;
                Assert.That(_door.CurrentDoorStatus, Is.EqualTo(testValue));
            };
            
        }
        [Test]
        public void CloseDoorEvent()
        {
            bool testValue;
            _door.DoorClosed();
            _door.DoorValueEvent += (o, args) =>
            {
                testValue = args._doorOpen;
                Assert.That(_door.CurrentDoorStatus, Is.EqualTo(testValue));
            };

        }
    }

}
