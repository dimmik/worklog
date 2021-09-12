using System;
using System.Collections.Generic;

namespace WorklogDomain
{
    public class Record : IEquatable<Record>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public DateTimeOffset Timestamp { get; set; } = DateTime.Now;
        public RecordMetadata Metadata { get; set; } = new RecordMetadata();
        public string Content { get; set; } = "";

        public override bool Equals(object obj)
        {
            return Equals(obj as Record);
        }

        public bool Equals(Record other)
        {
            return other != null &&
                   Id == other.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
        }
    }
}