using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WorklogDomain;
using WorklogStorage.Exceptions;

namespace WorklogStorage.InMemoryStorage
{
    public class PersistentLocalStorage : IWorklogStorage
    {
        private List<Notebook> NotebooksFromFile
        { 
            get
            {
                try
                {
                    var str = File.ReadAllText(Path);
                    var nbs = JsonConvert.DeserializeObject<List<Notebook>>(str);
                    return nbs;
                } catch
                {
                    return new List<Notebook>();
                }
            }
        }
        private void StoreNotebooks(IEnumerable<Notebook> nbs)
        {
            var str = JsonConvert.SerializeObject(nbs, Formatting.Indented);
            File.WriteAllText(Path, str);
        }
        private readonly string Path;
        public PersistentLocalStorage(string path)
        {
            Path = path;
        }

        public void AddNotebook(Notebook nb)
        {
            var Notebooks = NotebooksFromFile;
            if (Notebooks.Exists(n => n.Id == nb.Id))
            {
                Notebooks.RemoveAll(n => n.Id == nb.Id);
            }
            Notebooks.Add(nb);
            StoreNotebooks(Notebooks);
        }

        public void AddRecord(string nbId, Record rec)
        {
            var Notebooks = NotebooksFromFile;
            var nb = Notebooks.FirstOrDefault(n => n.Id == nbId);
            if (nb == null) throw new NoSuchNotebookException();
            nb.Records.Add(rec);
            StoreNotebooks(Notebooks);
        }

        public Notebook GetNotebook(string id)
        {
            var Notebooks = NotebooksFromFile;
            return Notebooks.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Notebook> GetNotebooks(string namespaceMd5)
        {
            var Notebooks = NotebooksFromFile;
            return string.IsNullOrWhiteSpace(namespaceMd5) 
                ? Notebooks 
                : Notebooks.Where(n => n.NamespaceMd5 == namespaceMd5);
        }

        public void RemoveNotebook(string id)
        {
            var Notebooks = NotebooksFromFile;
            Notebooks.RemoveAll(n => n.Id == id);
            StoreNotebooks(Notebooks);
        }

        public void RemoveRecord(string nbId, string recId)
        {
            var Notebooks = NotebooksFromFile;
            var nb = Notebooks.FirstOrDefault(n => n.Id == nbId);
            if (nb == null) throw new NoSuchNotebookException();
            nb.Records.RemoveAll(r => r.Id == recId);
            StoreNotebooks(Notebooks);
        }

        public void UpdateNotebook(string id, Notebook nb)
        {
            var Notebooks = NotebooksFromFile;
            var clone = JsonConvert.DeserializeObject<Notebook>(JsonConvert.SerializeObject(nb));
            clone.Id = id;
            AddNotebook(clone);
            StoreNotebooks(Notebooks);
        }

        public void UpdateRecord(string nbId, string recId, Record rec)
        {
            var Notebooks = NotebooksFromFile;
            var nb = Notebooks.FirstOrDefault(n => n.Id == nbId);
            if (nb == null) throw new NoSuchNotebookException();
            var clone = JsonConvert.DeserializeObject<Record>(JsonConvert.SerializeObject(rec));
            clone.Id = recId;
            nb.Records.RemoveAll(r => r.Id == recId);
            nb.Records.Add(clone);
            StoreNotebooks(Notebooks);
        }
    }
}
