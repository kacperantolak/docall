using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Doctor
{
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
public string id {get; set;}
public string first_name { get; set; }
public string last_name { get; set; }
public string bio { get; set; }
}