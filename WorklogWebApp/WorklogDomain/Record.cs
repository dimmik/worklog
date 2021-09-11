using System;

namespace WorklogDomain
{
    public class Record
    {
        public string Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public RecordMetadata Metadata { get; set; }
        public string Content { get; set; }
    }
}