using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class OfficeBooking
{
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
public string id {get; set;}
public string office_id { get; set; }
public string doctor_id { get; set; }
public TimeOnly time_start { get; set; }
public TimeOnly time_end { get; set; }
public DateTime created_at { get; set; }
public string week_day { get; set; }
}