using System.Reflection.Metadata;
using MongoDB.Driver;

public class BuildingService
{
private readonly MongoDbHelper _db;

public BuildingService(MongoDbHelper db)
{
_db = db;
}

public async Task<List<Building>> GetAllBuildings()
{
return await _db.GetCollection<Building>("building").Find(_ => true).ToListAsync();
}

public async Task AddBuilding(Building building)
{
    await _db.GetCollection<Building>("building").InsertOneAsync(building);
}
public async Task EditBuilding(Building building)
    {
        var filter = Builders<Building>.Filter.Eq(b => b.id, building.id);

        var update = Builders<Building>.Update.Combine(
            Builders<Building>.Update.Set(b => b.name, building.name),
            Builders<Building>.Update.Set(b => b.street, building.street),
            Builders<Building>.Update.Set(b => b.number, building.number),
            Builders<Building>.Update.Set(b => b.city, building.city),
            Builders<Building>.Update.Set(b => b.postcode, building.postcode)
        );

        _db.GetCollection<Building>("building").UpdateOne(filter, update);
    }

public DeleteResult DeleteBuilding(Building building)
    {
        var filter = Builders<Building>.Filter.Eq(b => b.id, building.id);
        return _db.GetCollection<Building>("building").DeleteOne(filter);
    }

}