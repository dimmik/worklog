﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorklogDomain;
using WorklogStorage.Exceptions;

namespace WorklogStorage.InMemoryStorage
{
    public class InMemoryWorklogStorage : IWorklogStorage
    {
        private readonly List<Notebook> Notebooks;
        public InMemoryWorklogStorage(IEnumerable<Notebook> notebooks)
        {
            Notebooks = notebooks.ToList();
        }

        public void AddNotebook(Notebook nb)
        {
            if (Notebooks.Exists(n => n.Id == nb.Id))
            {
                Notebooks.RemoveAll(n => n.Id == nb.Id);
            }
            Notebooks.Add(nb);
        }

        public void AddRecord(string nbId, Record rec)
        {
            var nb = Notebooks.FirstOrDefault(n => n.Id == nbId);
            if (nb == null) throw new NoSuchNotebookException();
            nb.Records.Add(rec);
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

        public void RemoveNotebook(string id)
        {
            Notebooks.RemoveAll(n => n.Id == id);
        }

        public void RemoveRecord(string nbId, string recId)
        {
            var nb = Notebooks.FirstOrDefault(n => n.Id == nbId);
            if (nb == null) throw new NoSuchNotebookException();
            nb.Records.RemoveAll(r => r.Id == recId);
        }

        public void UpdateNotebook(string id, Notebook nb)
        {
            var clone = JsonConvert.DeserializeObject<Notebook>(JsonConvert.SerializeObject(nb));
            clone.Id = id;
            AddNotebook(clone);
        }

        public void UpdateRecord(string nbId, string recId, Record rec)
        {
            var nb = Notebooks.FirstOrDefault(n => n.Id == nbId);
            if (nb == null) throw new NoSuchNotebookException();
            var clone = JsonConvert.DeserializeObject<Record>(JsonConvert.SerializeObject(rec));
            clone.Id = recId;
            nb.Records.RemoveAll(r => r.Id == recId);
            nb.Records.Add(clone);
        }
    }
}
