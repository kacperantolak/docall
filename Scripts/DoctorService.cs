using System.Reflection.Metadata;
using MongoDB.Driver;

public class DoctorService
{
private readonly MongoDbHelper _db;

public DoctorService(MongoDbHelper db)
{
_db = db;
}

public async Task<List<Doctor>> GetAllDoctors()
{
return await _db.GetCollection<Doctor>("doctor").Find(_ => true).ToListAsync();
}

public async Task AddDoctor(Doctor doctor)
{
    await _db.GetCollection<Doctor>("doctor").InsertOneAsync(doctor);
}
public async Task EditDoctor(Doctor doctor)
    {
        var filter = Builders<Doctor>.Filter.Eq(b => b.id, doctor.id);

        var update = Builders<Doctor>.Update.Combine(
            Builders<Doctor>.Update.Set(b => b.first_name, doctor.first_name),
            Builders<Doctor>.Update.Set(b => b.last_name, doctor.last_name),
            Builders<Doctor>.Update.Set(b => b.bio, doctor.bio)
        );

        _db.GetCollection<Doctor>("doctor").UpdateOne(filter, update);
    }

public DeleteResult DeleteDoctor(Doctor doctor)
    {
        var filter = Builders<Doctor>.Filter.Eq(b => b.id, doctor.id);
        return _db.GetCollection<Doctor>("doctor").DeleteOne(filter);
    }

}