﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WorklogDomain
{
    public class Notebook : IEquatable<Notebook>
    {
        public string Id { get; set; }
        public DateTimeOffset Created { get; set; } = DateTimeOffset.Now;
        public string SecretMd5 { get; set; }
        public string Name { get; set; }
        public IEnumerable<Record> Recods { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Notebook);
        }

        public bool Equals(Notebook other)
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
