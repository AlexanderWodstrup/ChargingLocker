using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingLocker;
using UsbSimulator;

namespace ChargingLocker
{
    public class LogWriter
    {
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public void LogDoorLocked(id)
        {
            
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
            }
        }

        public void LogDoorUnlocked(id)
        {

        }
    }
}
