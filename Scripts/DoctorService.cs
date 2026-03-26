using System.Text;
using Konscious.Security.Cryptography;
using MongoDB.Driver;

public class DoctorService
{
private readonly MongoDbHelper _db;

public DoctorService(MongoDbHelper db)
{
_db = db;
}
public async Task<Doctor> GetDoctorById(string id)
{
    var filter = Builders<Doctor>.Filter.Eq(d => d.id, id);
    return await _db.GetCollection<Doctor>("doctor").Find(filter).FirstOrDefaultAsync<Doctor>();
}
public async Task<List<Doctor>> GetAllDoctors()
{
return await _db.GetCollection<Doctor>("doctor").Find(_ => true).ToListAsync();
}

public async Task AddDoctor(Doctor doctor)
{
    byte[] salt = GenerateSalt(16);
    doctor.password = Convert.ToBase64String(HashPassword(doctor.password, salt));
    await _db.GetCollection<Doctor>("doctor").InsertOneAsync(doctor);
}
public async Task EditDoctor(Doctor doctor)
    {
        var filter = Builders<Doctor>.Filter.Eq(b => b.id, doctor.id);

        var update = Builders<Doctor>.Update.Combine(
            Builders<Doctor>.Update.Set(b => b.first_name, doctor.first_name),
            Builders<Doctor>.Update.Set(b => b.last_name, doctor.last_name),
            Builders<Doctor>.Update.Set(b => b.bio, doctor.bio),
            Builders<Doctor>.Update.Set(b => b.email, doctor.email)
        );

        _db.GetCollection<Doctor>("doctor").UpdateOne(filter, update);
    }

public DeleteResult DeleteDoctor(Doctor doctor)
    {
        var filter = Builders<Doctor>.Filter.Eq(b => b.id, doctor.id);
        return _db.GetCollection<Doctor>("doctor").DeleteOne(filter);
    }
    private static byte[] GenerateSalt(int length)
    {
        byte[] salt = new byte[length];
        using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }
        private static byte[] HashPassword(string password, byte[] salt)
    {
        using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            hasher.Salt = salt;
            hasher.DegreeOfParallelism = 8; // Number of threads
            hasher.MemorySize = 65536; // 64 MB of memory
            hasher.Iterations = 4; // Number of iterations
            return hasher.GetBytes(32); // Get 32-byte hash
        }
    }
}