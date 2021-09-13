using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using WorklogDomain;

namespace WorklogStorage.MongoDb
{
    public class MongoDbStorage : IWorklogStorage
    {
        private readonly MongoClient client;

        public MongoDbStorage(string url, string username, string password)
        {
            client = new MongoClient($"mongodb+srv://{HttpUtility.UrlEncode(username)}:{HttpUtility.UrlEncode(password)}@{url}?connect=replicaSet");
            var nb = GetNotebook("no-id");
        }

        public void AddNotebook(Notebook nb)
        {
            GetCollection().InsertOne(nb);
        }

        public void AddRecord(string nbId, Record rec)
        {
            var nb = GetNotebook(nbId);
            nb.Records.Add(rec);
            UpdateNotebook(nbId, nb);
        }

        public Notebook GetNotebook(string id)
        {
            return GetCollection().Find(n => n.Id == id).FirstOrDefault();
        }

        public IEnumerable<Notebook> GetNotebooks(string namespaceMd5)
        {
            var nbs = GetCollection()
                .AsQueryable()
                .Where(n => n.NamespaceMd5 == namespaceMd5)
                .OrderByDescending(n => n.Created);
            return nbs;
        }

        public void RemoveNotebook(string id)
        {
            GetCollection().DeleteOne(n => n.Id == id);
        }

        public void RemoveRecord(string nbId, string recId)
        {
            var nb = GetNotebook(nbId);
            nb.Records.RemoveAll(r => r.Id == recId);
            UpdateNotebook(nbId, nb);
        }

        public void UpdateNotebook(string id, Notebook nb)
        {
            var clone = JsonConvert.DeserializeObject<Notebook>(JsonConvert.SerializeObject(nb));
            clone.Id = id;
            GetCollection().ReplaceOne(n => n.Id == id, clone);

        }

        public void UpdateRecord(string nbId, string recId, Record rec)
        {
            var nb = GetNotebook(nbId);
            var clone = JsonConvert.DeserializeObject<Record>(JsonConvert.SerializeObject(rec));
            clone.Id = recId;
            nb.Records.RemoveAll(r => r.Id == recId);
            nb.Records.Add(clone);
            UpdateNotebook(nbId, nb);
        }

        private IMongoCollection<Notebook> GetCollection()
        {
            var db = client.GetDatabase("notebook");
            var coll = db.GetCollection<Notebook>("Notebooks");
            return coll;
        }
    }
}
