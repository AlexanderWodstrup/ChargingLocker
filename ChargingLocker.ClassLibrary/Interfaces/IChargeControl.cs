using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker.ClassLibrary
{
    interface IChargeControl
    {
        bool isConnected();
        void StartCharge();
        void StopCharge();
    }
}
