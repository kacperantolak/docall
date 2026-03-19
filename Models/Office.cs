using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Office
{
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
public string id {get; set;}
public string building_id { get; set; }
public string room_number { get; set; }
}