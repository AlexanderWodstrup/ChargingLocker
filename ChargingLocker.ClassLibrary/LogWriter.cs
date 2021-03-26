using System;
using System.IO;
using ChargingLocker.ClassLibrary.Interfaces;

namespace ChargingLocker.ClassLibrary
{
    public class LogWriter : ILogWriter
    {
        private string path = System.IO.Directory.GetCurrentDirectory();
        static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        
        private string logFile = projectDirectory + @"\Logfile.txt"; // Navnet på systemets log-fil
        public string msg { get; private set; }
        public string logLine { get; private set; }

        public void LogDoorLocked(int id)
        {
#if DEBUG
            Console.WriteLine("DEBUG:::LogDoorLocked called {0}.", id);
            Console.WriteLine("DEBUG:::Path = {0}", path);
            Console.WriteLine("DEBUG:::Path = {0}", projectDirectory);
#endif
            msg = "Door Locked with RFID: " + id.ToString();
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
            msg = "Door Unlocked with RFID: " + id.ToString();
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
            msg = "RFID: " + id.ToString() + " tried to unlock door";
            using (StreamWriter w = File.AppendText(logFile))
            {
                Log(msg, w);
            }
        }

        public void ReadFromLog()
        {
            using (StreamReader r = File.OpenText(logFile))
            {
                logLine = DumpLog(r,msg);
            }
        }

        public static string DumpLog(StreamReader r, string msg)
        {
            string line;
            while ((line = r.ReadLine()) != msg)
            {
                //do nothing
            }

            return line;
        }

        public void ClearLog()
        {
            File.WriteAllText(logFile, String.Empty);
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

        public void ReadFromLog()
        {
            using (StreamReader r = File.OpenText(logFile))
            {
                ReadFromLog(r);
            }


        }

        public void ReadFromLog(StreamReader r)
        {
            string[] lines = System.IO.File.ReadAllLines(logFile);
            //int lineCounter = 0;
            //foreach (string line in lines)
            //{
            //    //string _readFromLog;
            //    //if(lines[2] == "Door Locked with RFID: ")
            //    string read = lines[2+lineCounter];
            //    //Console.WriteLine("Check 1");
            //    StringComparison comp = StringComparison.OrdinalIgnoreCase;
            //    string OutputString = comp + ": " + line.Contains(read, comp);
            //    Console.WriteLine(OutputString);
            //    lineCounter += 5;
            //}
            string line;
            int cnt = lines.Length - 3;

            string OutputString;
            while ((line = r.ReadLine()) != null)
            {
                OutputString = lines[cnt];
            }
        }
    }
}
