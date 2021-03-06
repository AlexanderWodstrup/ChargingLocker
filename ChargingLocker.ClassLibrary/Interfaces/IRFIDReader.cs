using System;

namespace ChargingLocker.ClassLibrary
{
    public class RFIDEventArgs : EventArgs
    {
        public int id { set; get; }
    }
    public interface IRFIDReader
    {
        event EventHandler<RFIDEventArgs> RFIDValueEvent;
        int IdValue { get; }
        public void ReadRFID(int id);
    }
}
