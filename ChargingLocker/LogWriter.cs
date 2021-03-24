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
        private string logFile = @"C:\Users\Rizl\Documents\Skole\4. Semester\SWT\ChargingLocker\ChargingLocker\logfile.txt"; // Navnet på systemets log-fil
        
        public void LogDoorLocked(int id)
        {
#if DEBUG
            Console.WriteLine("DEBUG:::LogDoorLocked called {0}.", id);
#endif
            string msg = "Door Locked with RFID: " + id.ToString();
            using (StreamWriter w = File.AppendText(logFile))
            {
                Log(msg, w);
            }
            
        }

        public void LogDoorUnlocked(int id)
        {
#if DEBUG
            Console.WriteLine("DEBUG:::LogDoorUnlocked called {0}.", id);
#endif
            string msg = "Door Unlocked with RFID: " + id.ToString();
            using (StreamWriter w = File.AppendText(logFile))
            {
                Log(msg, w);
            }
        }

        public void LogDoorTriedUnlockedWithWrongId(int id)
        {
#if DEBUG
            Console.WriteLine("DEBUG:::LogDoorTriedUnlockedWithWrongId called {0}.", id);
#endif
            string msg = "RFID: " + id.ToString() + " tried to unlock door";
            using (StreamWriter w = File.AppendText(logFile))
            {
                Log(msg, w);
            }
        }


        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("------------------------------------");
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine($"{logMessage}");
            w.WriteLine("------------------------------------");
            w.WriteLine();
        }
    }
}
