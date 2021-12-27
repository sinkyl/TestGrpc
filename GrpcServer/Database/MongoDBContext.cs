using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace GrpcServer.Database
{
    public class MongoDBContext : IMongoDBContext
    {
        public IMongoClient Client { get; }

        public IMongoDatabase Database { get; }

        public static Dictionary<string, string> CollectionList { get; private set; }

        public MongoDBContext(IOptions<MongoDBModel> mongoSettings)
        {
            Client = new MongoClient(mongoSettings.Value.ConnectionString);
            Database = Client.GetDatabase(mongoSettings.Value.Database);
            // recieve existing collections from the json file
            CollectionList = mongoSettings.Value.Collections;
        }

        public MongoDBContext(string connectionString, string database)
        {
            Client = new MongoClient(connectionString);
            Database = Client.GetDatabase(database);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            IMongoCollection<T> coll = null;

            var collName = GetCollectionName<T>();

            if (collName != typeof(T).Name)
            {
                coll = Database.GetCollection<T>(collName);
            }

            return coll;
        }

        public static string GetCollectionName<T>()
        {
            var typeName = typeof(T).Name;

            foreach (var collection in CollectionList)
            {
                if (typeName == collection.Key)
                {
                    return collection.Value;
                }
            }

            return typeName;
        }
    }

    public interface IMongoDBContext
    {
        IMongoClient Client { get; }
        IMongoDatabase Database { get; }
        IMongoCollection<T> GetCollection<T>();
    }

    [JsonObject("mongoDBModel")]
    public class MongoDBModel
    {
        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }
        [JsonProperty("database")]
        public string Database { get; set; }
        [JsonProperty("collections")]
        public Dictionary<string, string> Collections { get; set; }
    }
}