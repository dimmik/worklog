using System;
using System.Collections.Generic;
using System.Text;
using WorklogDomain;

namespace WorklogStorage
{
    public interface IWorklogStorage
    {
        Notebook GetNotebook(string id);
        void StoreNotebook(Notebook nb);
    }
}
