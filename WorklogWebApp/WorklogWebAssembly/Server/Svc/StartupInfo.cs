using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorklogWebAssembly.Server.Svc
{
    public class StartupInfo
    {
        public readonly DateTimeOffset StartTime = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(3));
        public DateTimeOffset LastWakeup = DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(3));
    }
}
