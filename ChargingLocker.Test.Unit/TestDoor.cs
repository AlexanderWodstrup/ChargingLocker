using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker.ClassLibrary;
using NUnit.Framework;

namespace ChargingLocker.Test.Unit
{
    [TestFixture]
    public class TestDoor
    {
        private IDoor _uut;
        private DoorEventArgs _receivedEventArgs;
        
        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new Door();
            
            _uut.DoorValueEvent += (o, args) =>
            {
                _receivedEventArgs = args;
            };
        }

        [Test]
        public void Test_Of_LockDoor()
        {
            _uut.LockDoor();
            Assert.That(_uut._lock,Is.EqualTo(true));
        }

        [Test]
        public void Test_Of_UnlockDoor()
        {
            _uut.UnlockDoor();
            Assert.That(_uut._lock, Is.EqualTo(false));
        }

        [Test]
        public void Test_Of_DoorOpened_While_lock_True()
        {
            _uut.LockDoor();
            _uut.DoorOpened();
            //Assert.That(Console.Out,Is.EqualTo("The door is locked, please scan your RFID tag"));
        }

        [Test]
        public void CurrentDoorStatus_DoorOpened_While_lock_false()
        {
            _uut.DoorOpened();
            Assert.That(_uut.CurrentDoorStatus, Is.EqualTo(true));
        }

        [Test]
        public void DoorValueEvent_DoorOpened_While_lock_false_EventFired()
        {
            _uut.DoorOpened();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void DoorValueEvent_DoorOpened_While_lock_false_CorrectEventReceived()
        {
            _uut.DoorOpened();
            Assert.That(_receivedEventArgs._doorOpen, Is.EqualTo(true));
        }
        [Test]
        public void Test_Of_DoorClosed_While_lock_True()
        {
            _uut.LockDoor();
            _uut.DoorClosed();
            //Assert.That(Console.Out,Is.EqualTo("The door is closed and locked, please scan your RFID tag"));
        }

        [Test]
        public void CurrentDoorStatus_DoorClosed_While_lock_false()
        {
            _uut.DoorClosed();
            Assert.That(_uut.CurrentDoorStatus, Is.EqualTo(false));
        }

        [Test]
        public void DoorValueEvent_DoorClosed_While_lock_false_EventFired()
        {
            _uut.DoorClosed();
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void DoorValueEvent_DoorClosed_While_lock_false_CorrectEventReceived()
        {
            _uut.DoorClosed();
            Assert.That(_receivedEventArgs._doorOpen, Is.EqualTo(false));
        }
    }
}
