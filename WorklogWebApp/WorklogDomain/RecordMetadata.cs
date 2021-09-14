using System.Collections.Generic;

namespace WorklogDomain
{
    public class RecordMetadata
    {
        public bool Encrypted { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}