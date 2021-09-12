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
        public IEnumerable<Notebook> Get()
        {
            return Storage.GetNotebooks(null);
        }

        [HttpGet("{id}")]
        public Notebook Get(string id)
        {
            return Storage.GetNotebook(id);
        }

        [HttpPost]
        public void Post([FromBody] Notebook nb)
        {
            Storage.AddNotebook(nb);
        }


        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Storage.RemoveNotebook(id);
        }
    }
}
