using System.Reflection.Metadata;
using MongoDB.Driver;

public class OfficeService
{
private readonly MongoDbHelper _db;

public OfficeService(MongoDbHelper db)
{
_db = db;
}

public async Task<List<Office>> GetOfficeByBuilding(string id)
{
    var filter = Builders<Office>.Filter.Eq("building_id", id);
    return await _db.GetCollection<Office>("office").Find(filter).ToListAsync();
}

public async Task AddOffice(Office office)
{
    await _db.GetCollection<Office>("office").InsertOneAsync(office);
}
public async Task EditOffice(Office office)
    {
        var filter = Builders<Office>.Filter.Eq("building_id", office.building_id);

        var update = Builders<Office>.Update.Combine(
            Builders<Office>.Update.Set(b => b.room_number, office.room_number)
        );

        _db.GetCollection<Office>("office").UpdateOne(filter, update);
    }

public DeleteResult DeleteOffice(Office office)
    {
        var filter = Builders<Office>.Filter.Eq("id", office.id);
        return _db.GetCollection<Office>("office").DeleteOne(filter);
    }

}