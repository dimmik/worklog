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
        // GET: api/<NoebookController>
        [HttpGet("list")]
        public IEnumerable<Notebook> Get()
        {
            return Storage.GetNotebooks(null);
        }

        // GET api/<NoebookController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NoebookController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<NoebookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NoebookController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
