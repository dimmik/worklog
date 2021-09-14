using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorklogDomain;
using WorklogStorage;
using WorklogWebAssembly.Server.Auth;

namespace WorklogWebAssembly.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Log;
        private readonly IWorklogStorage Storage;

        public AuthController(IConfiguration config, ILogger<AuthController> logger, IWorklogStorage storage)
        {
            Configuration = config;
            Log = logger;
            Storage = storage;
        }
        [HttpGet]
        public AuthData GetAuthData()
        {
            var authDataFromCookie = HttpContext.Request.GetAuthDataFromCookie(Configuration.GetValue<string>("AdminPassword"));
            if (authDataFromCookie.IsAuthorized && !authDataFromCookie.IsAdmin)
            {
                authDataFromCookie.IsAuthorized = Storage.GetNotebooks(authDataFromCookie.NamespaceMd5).Any();
                if (!authDataFromCookie.IsAuthorized) Logout();
            }
            return authDataFromCookie;
        }
        [HttpPost]
        public string SetAuthCookie([FromBody]string ns)
        {
            HttpContext.Response.Cookies.Append("Namespace", ns, new CookieOptions() { Expires = DateTime.Now.AddYears(100) });
            return "ok";
        }
        [HttpGet("logout")]
        public string Logout()
        {
            HttpContext.Response.Cookies.Append("Namespace", "", new CookieOptions() { Expires = DateTime.Now.AddYears(100) });
            return "logged out";
        }

    }
}
