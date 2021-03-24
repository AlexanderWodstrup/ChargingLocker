using System;
using System.IO;

namespace ChargingLocker.ClassLibrary
{
    public class LogWriter
    {
        private string path = System.IO.Directory.GetCurrentDirectory();
        static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        
        private string logFile = projectDirectory + @"\Logfile.txt"; // Navnet på systemets log-fil
        
        public void LogDoorLocked(int id)
        {
#if DEBUG
            Console.WriteLine("DEBUG:::LogDoorLocked called {0}.", id);
            Console.WriteLine("DEBUG:::Path = {0}", path);
            Console.WriteLine("DEBUG:::Path = {0}", projectDirectory);
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
