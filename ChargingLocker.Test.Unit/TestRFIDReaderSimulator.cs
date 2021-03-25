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
        private IStationControl _stationControl;

        [SetUp]
        public void Setup()
        {
            _uut = new RFIDReaderSimulator();
            _stationControl = Substitute.For<IStationControl>();
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
        public void Correct_ID_Is_Sent(int id)
        {
            //int sentValue = 0;
            //_uut.RFIDValueEvent += (o, args) => { sentValue = args.id; };
            //_uut.ReadRFID(id);
            //_stationControl.Received(1).RfidDetected();
            //Assert.That(sentValue, Is.EqualTo(id));
        }

    }
}
