using MongoDB.Driver;

public class MongoDbHelper
{
private readonly IMongoDatabase _db;

public MongoDbHelper()
{
var client = new MongoClient("mongodb://localhost:27017/?directConnection=true");
_db = client.GetDatabase("docall");
}

public IMongoCollection<T> GetCollection<T>(string name)
{
return _db.GetCollection<T>(name);
}
}