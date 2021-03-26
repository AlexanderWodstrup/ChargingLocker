using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker.ClassLibrary;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace ChargingLocker.Test.Unit
{
    [TestFixture]
    public class TestDisplay
    {
        private IDisplay _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }
        [Test]
        public void Test_Of_DisplayConnectPhone()
        {
            _uut.DisplayConnectPhone();
            Assert.That(_uut.OutputString,Is.EqualTo("Please connect your phone"));
        }
        [Test]
        public void Test_Of_DisplayScanRFID()
        {
            _uut.DisplayScanRFID();
            Assert.That(_uut.OutputString, Is.EqualTo("Please scan your tag"));
        }

        [Test]
        public void Test_Of_DisplayFailedConnection()
        {
            _uut.DisplayFailedConnection();
            Assert.That(_uut.OutputString, Is.EqualTo("The system could not connect to your device, please try again"));
        }
        [Test]
        public void Test_Of_DisplayChargeLockerOccupied()
        {
            _uut.DisplayChargeLockerOccupied();
            Assert.That(_uut.OutputString, Is.EqualTo("This locker is now occupied by you"));
        }
        [Test]
        public void Test_Of_DisplayWrongRFID()
        {
            _uut.DisplayWrongRFID();
            Assert.That(_uut.OutputString, Is.EqualTo("Your tag could not be found, please try again"));
        }
        [Test]
        public void Test_Of_DisplayRemovePhone()
        {
            _uut.DisplayRemovePhone();
            Assert.That(_uut.OutputString, Is.EqualTo("Please remove your device"));
        }
        [Test]
        public void Test_Of_DisplayDoorOpen()
        {
            _uut.DisplayDoorOpen();
            Assert.That(_uut.OutputString, Is.EqualTo("Please close the door before scaning your RFID tag"));
        }
        [TestCase(25)]
        public void Test_Of_DisplayPhoneCharging(double current)
        {
            _uut.DisplayPhoneCharging(current);
            string testString = "Your device is now charging: " + current + "%";
            Assert.That(_uut.OutputString, Is.EqualTo(testString));
        }
        [Test]
        public void Test_Of_DisplayPhoneFullyCharged()
        {
            _uut.DisplayPhoneFullyCharged();
            Assert.That(_uut.OutputString, Is.EqualTo("Your device is now fully charged"));
        }

        [Test]
        public void Test_Of_DisplayDoorIsLocked()
        {
            _uut.DisplayDoorIsLocked();
            Assert.That(_uut.OutputString, Is.EqualTo("The door is locked, please scan your RFID tag"));
        }

    }
}
