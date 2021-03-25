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
        private Door _door;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<Door>();

            _uut = new StationControl(_door,_usbCharger);
        }

        [Test]
        public void OpenDoor()
        {
            _door.DoorOpened();
            
            Assert.That(_door.CurrentDoorStatus, Is.EqualTo(true));
        }
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
    }

}
