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
    var filter = Builders<Office>.Filter.Eq(o => o.building_id, id);
    return await _db.GetCollection<Office>("office").Find(filter).ToListAsync();
}
public async Task<Office> GetOfficeById(string id)
{
    var filter = Builders<Office>.Filter.Eq("id", id);
    return await _db.GetCollection<Office>("office").Find(filter).FirstOrDefaultAsync<Office>();
}
public async Task AddOffice(Office office)
{
    await _db.GetCollection<Office>("office").InsertOneAsync(office);
}
public async Task EditOffice(Office office)
    {
        var filter = Builders<Office>.Filter.Eq("id", office.id);

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
public async Task<List<OfficeBooking>> GetOfficeBookingsByOffice(string id)
    {
        var filter = Builders<OfficeBooking>.Filter.Eq("office_id", id);
        return await _db.GetCollection<OfficeBooking>("office_booking").Find(filter).ToListAsync();
    }
    public async Task<List<OfficeBooking>> GetOfficeBookingsByDoctor(string id)
    {
        var filter = Builders<OfficeBooking>.Filter.Eq("doctor_id", id);
        return await _db.GetCollection<OfficeBooking>("office_booking").Find(filter).ToListAsync();
    }
public async Task BookOffice(OfficeBooking booking)
    {
    await _db.GetCollection<OfficeBooking>("office_booking").InsertOneAsync(booking);
    }
public async Task EditBooking(OfficeBooking booking)
    {
        var filter = Builders<OfficeBooking>.Filter.Eq(b => b.id, booking.id);

        var update = Builders<OfficeBooking>.Update.Combine(
            Builders<OfficeBooking>.Update.Set(b => b.time_start, booking.time_start),
            Builders<OfficeBooking>.Update.Set(b => b.time_end, booking.time_end),
            Builders<OfficeBooking>.Update.Set(b => b.week_day, booking.week_day)
        );

        _db.GetCollection<OfficeBooking>("office_booking").UpdateOne(filter, update);
    }
public DeleteResult DeleteBooking(OfficeBooking booking)
    {
        var filter = Builders<OfficeBooking>.Filter.Eq("id", booking.id);
        return _db.GetCollection<OfficeBooking>("office_booking").DeleteOne(filter);
    }
}