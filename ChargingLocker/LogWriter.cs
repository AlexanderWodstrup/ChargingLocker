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
            Console.WriteLine("LogDoorLocked called {0}.",id);
            string msg = "Door Locked with RFID: " + id.ToString();
            using (StreamWriter w = File.AppendText(logFile))
            {
                Log(msg, w);
            }
            
        }
        
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($" :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
    
}
