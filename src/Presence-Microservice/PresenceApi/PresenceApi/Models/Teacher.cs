using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PresenceApi.Models
{
	[BsonIgnoreExtraElements]
	public class Teacher
	{
		[BsonElement("Id")]
		[BsonRepresentation(BsonType.String)]
		public Guid Id { get; set; }
		[BsonElement("Name")]
		public string? Name { get; set; }
	}
}
