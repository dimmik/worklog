using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorklogWebAssembly.Server.Svc
{
    public class StartupInfo
    {
        public DateTimeOffset StartTime { get; set; } = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(3));
        public DateTimeOffset LastWakeup { get; set; } = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(3));
    }
}
