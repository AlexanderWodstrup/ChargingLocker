using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker;

namespace RFIDSimulator
{
    class RFIDReaderSimulator : IRFIDReader
    {
        public event EventHandler<RFIDEventArgs> RFIDValueEvent;
        public int IdValue { get; private set; }
        public RFIDReaderSimulator()
        {

        }
        public void ReadRFID(int id)
        {
            IdValue = id;
            RFIDValueEvent?.Invoke(this,new RFIDEventArgs() {id = this.IdValue});
            
        }
    }
}
