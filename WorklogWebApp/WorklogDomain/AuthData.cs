using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorklogDomain
{
    public class AuthData
    {
        public bool IsAdmin { get; set; }
        public bool IsAuthorized { get; set; }
        public string NamespaceMd5 { get; set; }
    }
}
