using System;
using System.Collections.Generic;
using System.Text;

namespace WorklogDomain
{
    public class Notebook
    {
        public string Id { get; set; }
        public string SecretMd5 { get; set; }
        public string Name { get; set; }
        public IEnumerable<Record> Recods { get; set; }
    }
}
