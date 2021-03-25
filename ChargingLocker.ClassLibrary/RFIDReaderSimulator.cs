using System;

namespace ChargingLocker.ClassLibrary
{
    public class RFIDReaderSimulator : IRFIDReader
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
