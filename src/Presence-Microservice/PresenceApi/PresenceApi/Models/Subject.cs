using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PresenceApi.Models
{
	[BsonIgnoreExtraElements]
	public class Subject
	{
		[BsonElement("Id")]
		[BsonRepresentation(BsonType.String)]
		public Guid Id { get; set; }
		[BsonElement("Description")]
		public string? Description { get; set; }
	}
}
