using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Building
{
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
public string id {get; set;}
public string name { get; set; }
public string street { get; set; }
public string number { get; set; }
public string city { get; set; }
public string postcode { get; set; }
}