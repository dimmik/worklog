using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorklogDomain;
using WorklogWebAssembly.Server.Auth;

namespace WorklogWebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Log;
        public AuthController(IConfiguration config, ILogger<AuthController> logger)
        {
            Configuration = config;
            Log = logger;
        }
        [HttpGet]
        public AuthData GetAuthData()
        {
            return HttpContext.Request.GetAuthData(Configuration.GetValue<string>("AdminPassword"));
        }
        [HttpPost]
        public string SetAuthCookie([FromBody]string ns)
        {
            HttpContext.Response.Cookies.Append("Namespace", ns, new CookieOptions() { Expires = DateTime.Now.AddYears(100) });
            return "ok";
        } 
    }
}
