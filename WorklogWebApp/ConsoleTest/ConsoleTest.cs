using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using WorklogDomain;
using WorklogStorage;
using WorklogStorage.InMemoryStorage;

namespace ConsoleTest
{
    class ConsoleTest
    {
        static void Main()
        {
            string data = File.ReadAllText("./test-inmem-nbs.json");
            Notebook[] n = JsonConvert.DeserializeObject<Notebook[]>(data);
            IWorklogStorage st = new PersistentLocalStorage(n);
            var nb = st.GetNotebook("n1Id");
        }
        static void MainGenTestNb(string[] args)
        {
            Notebook[] n = GetTestNbs();
            string nj = JsonConvert.SerializeObject(n, Formatting.Indented);
            var path = Path.GetFullPath("./test-inmem-nbs.json");
            File.WriteAllText(path, nj);

        }

        private static Notebook[] GetTestNbs()
        {
            return new[] {
                new Notebook()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Notebook test inmem 1",
                    NamespaceMd5 = "d7d3dcd12db9be367089dd914a41f820", // test-notebooks
                    Records = new List<Record>() {
                        new Record()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Timestamp = DateTimeOffset.Now,
                            Metadata = new RecordMetadata()
                            {
                                Encrypted = false,
                                Tags = new[] { "first", "test", "fun" }
                            },
                            Content = "Notebook 1 Record 1"
                        },
                        new Record()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Timestamp = DateTimeOffset.Now,
                            Metadata = new RecordMetadata()
                            {
                                Encrypted = false,
                                Tags = new[] { "second", "test", "fun" }
                            },
                            Content = "Notebook 1 Record 2"
                        },
                        new Record()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Timestamp = DateTimeOffset.Now,
                            Metadata = new RecordMetadata()
                            {
                                Encrypted = false,
                                Tags = new[] { "third", "test", "fun" }
                            },
                            Content = "Notebook 1 Record 3"
                        },
                    }
                },
                new Notebook()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Notebook test inmem 1",
                    NamespaceMd5 = "d7d3dcd12db9be367089dd914a41f820", // test-notebooks,
                    Records = new List<Record> {
                        new Record()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Timestamp = DateTimeOffset.Now,
                            Metadata = new RecordMetadata()
                            {
                                Encrypted = false,
                                Tags = new[] { "first", "test", "fun" }
                            },
                            Content = "Notebook 2 Record 1"
                        },
                        new Record()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Timestamp = DateTimeOffset.Now,
                            Metadata = new RecordMetadata()
                            {
                                Encrypted = false,
                                Tags = new[] { "second", "test", "fun" }
                            },
                            Content = "Notebook 2 Record 2"
                        },
                        new Record()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Timestamp = DateTimeOffset.Now,
                            Metadata = new RecordMetadata()
                            {
                                Encrypted = false,
                                Tags = new[] { "third", "test", "fun" }
                            },
                            Content = "Notebook 2 Record 3"
                        },
                    }
                }
            };
        }
    }
}
