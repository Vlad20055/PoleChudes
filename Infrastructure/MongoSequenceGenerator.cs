using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure;

public class MongoSequenceGenerator : ISequenceGenerator
{
    private readonly IMongoCollection<BsonDocument> _counters;

    public MongoSequenceGenerator(string connectionString, string dbName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(dbName);

        // Будем хранить документы вида { _id: sequenceName, seq: int }
        _counters = database.GetCollection<BsonDocument>("counters");
    }

    public async Task<int> GetNextIdAsync(string sequenceName)
    {
        var filter = Builders<BsonDocument>.Filter.Eq("_id", sequenceName);
        var update = Builders<BsonDocument>.Update.Inc("seq", 1);

        var options = new FindOneAndUpdateOptions<BsonDocument>
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        var result = await _counters.FindOneAndUpdateAsync(filter, update, options);
        return result["seq"].AsInt32;
    }
}
