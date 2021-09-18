﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorklogWebAssembly.Server.Controllers
{
    public class StartupInfo
    {
        public DateTimeOffset StartTime { get; set; } = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(3));
        public List<DateTimeOffset> LastWakeups { get; set; } = new List<DateTimeOffset>();
        public int NumberWakeupsToKeep { get; set; } = 15;
        public string Uptime => $"{DateTimeOffset.Now - StartTime}";
        public void Wakeup()
        {
            if (LastWakeups.Count >= NumberWakeupsToKeep)
            {
                LastWakeups.RemoveAt(0);
            }
            LastWakeups.Add(DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(3)));
        }
    }
}
