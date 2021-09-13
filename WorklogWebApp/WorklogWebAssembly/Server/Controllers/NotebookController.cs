using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorklogDomain;
using WorklogStorage;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorklogWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotebookController : ControllerBase
    {
        private IConfiguration Configuration;
        private ILogger Log;
        private IWorklogStorage Storage;
        public NotebookController(IConfiguration config, ILogger<NotebookController> logger, IWorklogStorage storage)
        {
            Configuration = config;
            Log = logger;
            Storage = storage;
        }
        [HttpGet]
        public IEnumerable<Notebook> GetListOfNotebooks()
        {
            return Storage.GetNotebooks(null);
        }

        [HttpGet("{id}")]
        public Notebook GetNotebook(string id)
        {
            return Storage.GetNotebook(id);
        }

        [HttpPost]
        public void AddNotebook([FromBody] Notebook nb)
        {
            Storage.AddNotebook(nb);
        }


        [HttpDelete("{id}")]
        public void RemoveNotebook(string id)
        {
            Storage.RemoveNotebook(id);
        }

        #region records manipulation
        [HttpPost("{nbId}/record")]
        public void AddRecord([FromRoute] string nbId, [FromBody] Record rec)
        {
            Storage.AddRecord(nbId, rec);
        }
        [HttpDelete("{nbId}/record/{recId}")]
        public void RemoveRecord([FromRoute]string nbId, [FromRoute]string recId)
        {
            Storage.RemoveRecord(nbId, recId);
        }
        #endregion
    }
}
