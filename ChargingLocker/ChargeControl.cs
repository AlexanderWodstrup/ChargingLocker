using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker;
using UsbSimulator;

namespace ChargingLocker
{
    public class ChargeControl
    {
        private bool _connected= false;
        private IUsbCharger _charger;
        public ChargeControl()
        {

        }

        public bool isConnected()
        {
            return _connected;
        }

        public void StartCharge()
        {
            _charger.StartCharge();
        }

        public void StopCharge()
        {
            _charger.StopCharge();
        }
    }
}
