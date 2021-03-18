using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker
{
    public class RFIDEventArgs : EventArgs
    {
        public int id { set; get; }
    }
    public interface IRFIDReader
    {
        public event EventHandler<RFIDEventArgs> RFIDValueEvent;
        int IdValue { get; }
        public void ReadRFID(int id)
        {

        }
    }
}
