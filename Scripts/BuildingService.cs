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



}