using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker.ClassLibrary;
using NUnit.Framework;
using NSubstitute;

namespace ChargingLocker.Test.Unit
{
    [TestFixture]
    public class TestDoor
    {
        private IDoor _uut;
        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<IDoor>();
        }

        [Test]
        public void IsDoorOpened()
        {
            _uut.DoorOpened();
            _uut.CurrentDoorStatus.Returns(true);
        }
        
        [Test]
        public void IsDoorClosed()
        {
            _uut.DoorClosed();
            _uut.CurrentDoorStatus.Returns(false);
        }

        [Test]
        public void IsDoorLocked()
        {
            _uut.LockDoor();
            _uut.CurrentDoorStatus.Returns(true);
        }

        [Test]
        public void IsDoorUnlocked()
        {
            _uut.UnlockDoor();
            _uut.CurrentDoorStatus.Returns(false);
        }

        [Test]
        public void DoorStatus()
        {
            _uut.CurrentDoorStatus.Returns(true);
            Assert.That(_uut.CurrentDoorStatus, Is.EqualTo(true));
        }

        [Test]
        public void ValueEvent()
        {
            bool testValue;
            _uut.DoorOpened();
            _uut.DoorValueEvent += (o, args) =>
            {
                testValue = args._doorOpen;
                Assert.That(_uut.CurrentDoorStatus, Is.EqualTo(testValue));
            };
        }
    }
}