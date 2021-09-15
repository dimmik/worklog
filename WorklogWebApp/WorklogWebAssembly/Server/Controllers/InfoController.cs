using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorklogWebAssembly.Server.Svc;

namespace WorklogWebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly StartupInfo startupInfo;

        public InfoController(StartupInfo startupInfo)
        {
            this.startupInfo = startupInfo;
        }

        [HttpGet("start")]
        public DateTimeOffset GetStartupTime()
        {
            return startupInfo.StartTime;
        }
    }
}
