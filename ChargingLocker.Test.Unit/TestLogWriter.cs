using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker.ClassLibrary;
using ChargingLocker.ClassLibrary.Interfaces;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace ChargingLocker.Test.Unit
{
    [TestFixture]
    public class TestLogWriter
    {
        private ILogWriter _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new LogWriter();
        }

        [TestCase(25)]
        public void Test_Of_LogDoorLocked(int id)
        {
            _uut.LogDoorLocked(id);
            string tmp = "Door Locked with RFID: " + id.ToString();
            Assert.That(_uut.msg,Is.EqualTo(tmp));
        }

        [TestCase(25)]
        public void Test_Of_Log(int id)
        {
            _uut.ClearLog();
            _uut.LogDoorLocked(id);
            string tmp = "Door Locked with RFID: " + id.ToString();
            _uut.ReadFromLog();

            Assert.That(_uut.logLine, Is.EqualTo(tmp));
        }
        
    }
}
