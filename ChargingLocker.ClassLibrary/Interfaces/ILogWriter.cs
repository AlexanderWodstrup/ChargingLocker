using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingLocker.ClassLibrary.Interfaces
{
    public interface ILogWriter
    {
        public string msg { get; }
        public string logLine { get; }
        void LogDoorLocked(int id);
        void LogDoorUnlocked(int id);
        void LogDoorTriedUnlockedWithWrongId(int id);

        void ClearLog();
        void ReadFromLog();
    }
}
