using System;
using System.Collections.Generic;
using System.Text;
using WorklogDomain;

namespace WorklogStorage
{
    public interface IWorklogStorage
    {
        Notebook GetNotebook(string id);
        IEnumerable<Notebook> GetNotebooks(string namespaceMd5);
        void RemoveNotebook(string id);
        void AddNotebook(Notebook nb);
        void UpdateNotebook(string id, Notebook nb);
    }
}
