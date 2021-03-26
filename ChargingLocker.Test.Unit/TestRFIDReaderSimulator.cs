using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker.ClassLibrary;
using System.Threading;
using NUnit.Framework;
using NSubstitute;

namespace ChargingLocker.Test.Unit
{
    [TestFixture]
    public class TestRFIDReaderSimulator
    {
        private RFIDReaderSimulator _uut;
        private RFIDEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new RFIDReaderSimulator();
            

            _uut.RFIDValueEvent += (o, args) =>
            {
                _receivedEventArgs = args;
            };
        }
        
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(15)]
        public void Correct_ID_Is_Set(int id)
        {
            _uut.ReadRFID(id);
            Assert.That(_uut.IdValue, Is.EqualTo(id));
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(15)]
        public void RFIDValueEvent_ReadRFID_EventFired(int id)
        {
            _uut.ReadRFID(id);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(1)]
        [TestCase(0)]
        [TestCase(15)]
        public void RFIDValueEvent_ReadRFID_CorrectEventReceived(int id)
        {
            _uut.ReadRFID(id);
            Assert.That(_receivedEventArgs.id, Is.EqualTo(id));
        }

    }
}
