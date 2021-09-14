using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorklogDomain;
using WorklogStorage;
using WorklogWebApp.Exceptions;
using WorklogWebAssembly.Server.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorklogWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotebookController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger Log;
        private readonly IWorklogStorage Storage;
        public NotebookController(IConfiguration config, ILogger<NotebookController> logger, IWorklogStorage storage)
        {
            Configuration = config;
            Log = logger;
            Storage = storage;
        }
        [HttpGet]
        public IEnumerable<Notebook> GetListOfNotebooks()
        {
            AuthData authData = GetAuthDataAndThrowIfNotAuthorized();

            return Storage.GetNotebooks(authData.IsAdmin ? null : authData.NamespaceMd5);
        }

        private AuthData GetAuthDataAndThrowIfNotAuthorized()
        {
            var authData = HttpContext.Request.GetAuthDataFromCookie(Configuration.GetValue<string>("AdminPassword"));
            if (!authData.IsAuthorized) throw HttpException.NotAuthenticated("Not authenticated");
            return authData;
        }

        [HttpGet("{id}")]
        public Notebook GetNotebook(string id)
        {
            AuthData authData = GetAuthDataAndThrowIfNotAuthorized();
            return Storage.GetNotebook(id);
        }

        [HttpPost]
        public void AddNotebook([FromBody] Notebook nb)
        {
            AuthData authData = GetAuthDataAndThrowIfNotAuthorized();
            Storage.AddNotebook(nb);
        }


        [HttpDelete("{id}")]
        public void RemoveNotebook(string id)
        {
            AuthData authData = GetAuthDataAndThrowIfNotAuthorized();
            Storage.RemoveNotebook(id);
        }

        #region records manipulation
        [HttpPost("{nbId}/record")]
        public void AddRecord([FromRoute] string nbId, [FromBody] Record rec)
        {
            AuthData authData = GetAuthDataAndThrowIfNotAuthorized();
            Storage.AddRecord(nbId, rec);
        }
        [HttpDelete("{nbId}/record/{recId}")]
        public void RemoveRecord([FromRoute]string nbId, [FromRoute]string recId)
        {
            AuthData authData = GetAuthDataAndThrowIfNotAuthorized();
            Storage.RemoveRecord(nbId, recId);
        }
        #endregion
    }
}
