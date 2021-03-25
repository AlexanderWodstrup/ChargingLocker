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
    public class TestChargeControl
    {
        private ChargeControl _uut;
        private IUsbCharger _usbCharger;
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_display,_usbCharger);
        }

        [Test]
        public void Test_Of_StartChargeFunction()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
            
        }

        [Test]
        public void Test_Of_StopChargeFunction()
        {
            _uut.StopCharge();
            _usbCharger.Received().StopCharge();
        }
        
        [Test]
        public void ReadEventFunction_Display_DisplayConnecyPhone()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = 0});
            _display.Received(1).DisplayConnectPhone();
        }

        [Test]
        public void ReadEventFunction_ErrorCurrent_Display_DisplayFailedConnection()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 501 });

            _display.Received().DisplayFailedConnection();
        }
        [Test]
        public void ReadEventFunction_ErrorCurrent_Charger_StopCharge()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 501 });

            _usbCharger.Received().StopCharge();
        }
        [Test]
        public void ReadEventFunction_Display_DisplayPhoneFullyCharged()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 0 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 4 });

            _display.Received().DisplayPhoneFullyCharged();
        }
        [Test]
        public void ReadEventFunction_FullyChargedCurrent_Charger_StopCharge()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 0 });
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = 4 });

            _usbCharger.Received().StopCharge();
        }
    }
}
