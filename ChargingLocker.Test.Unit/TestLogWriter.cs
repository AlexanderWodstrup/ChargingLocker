using System;
using System.IO;
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
    public class TestLogWriter
    {
        private LogWriter _uut;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test_Of_Log_LockedDoor()
        {
            _uut.ReadFromLog();
        }
        
    }
}
