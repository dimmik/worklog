using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorklogWebAssembly.Server.Svc
{
    public class StartupInfo
    {
        public DateTimeOffset StartTime { get; set; } = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(3));
        public List<DateTimeOffset> LastWakeups { get; set; } = new List<DateTimeOffset>();
        public int numberWakeupsToKeep = 15;
        public void Wakeup()
        {
            if (LastWakeups.Count >= numberWakeupsToKeep)
            {
                LastWakeups.RemoveAt(0);
            }
            LastWakeups.Add(DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(3)));
        }
    }
}
