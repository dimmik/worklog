using System.Collections.Generic;

namespace WorklogDomain
{
    public class RecordMetadata
    {
        public bool Encrypted { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}