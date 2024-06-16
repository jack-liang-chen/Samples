using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDbSample.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Name")]
    public string Name { get; set; }

    [BsonElement("Age")]
    public int Age { get; set; }

    [BsonElement("Addresses")]
    public List<Address> Addresses { get; set; }
}
