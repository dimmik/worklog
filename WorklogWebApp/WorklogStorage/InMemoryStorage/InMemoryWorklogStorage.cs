using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorklogDomain;

namespace WorklogStorage.InMemoryStorage
{
    public class InMemoryWorklogStorage : IWorklogStorage
    {
        private readonly List<Notebook> Notebooks;
        public InMemoryWorklogStorage(IEnumerable<Notebook> notebooks)
        {
            Notebooks = notebooks.ToList();
        }
        public Notebook GetNotebook(string id)
        {
            return Notebooks.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Notebook> GetNotebooks(string namespaceMd5)
        {
            return string.IsNullOrWhiteSpace(namespaceMd5) 
                ? Notebooks 
                : Notebooks.Where(n => n.NamespaceMd5 == namespaceMd5);
        }

        public void StoreNotebook(Notebook nb)
        {
            if (Notebooks.Exists(n => n.Id == nb.Id))
            {
                Notebooks.Remove(Notebooks.Where(n => n.Id == nb.Id).First());
            }
            Notebooks.Add(nb);
        }
    }
}
