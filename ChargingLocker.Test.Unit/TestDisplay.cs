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
    public class TestDisplay
    {
        private IDisplay _uut;
        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<IDisplay>();
        }

        [Test]
        public void IsPhoneConnected()
        {

        }
    }
}
