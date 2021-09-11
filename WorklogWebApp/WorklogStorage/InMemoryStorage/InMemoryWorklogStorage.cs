using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorklogDomain;

namespace WorklogStorage.InMemoryStorage
{
    public class InMemoryWorklogStorage : IWorklogStorage
    {
        private readonly IEnumerable<Notebook> Notebooks;
        public InMemoryWorklogStorage(IEnumerable<Notebook> notebooks)
        {
            Notebooks = notebooks;
        }
        public Notebook GetNotebook(string id)
        {
            return Notebooks.FirstOrDefault(n => n.Id == id);
        }
    }
}
