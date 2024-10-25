using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace PresenceApi.Models
{
	[Collection("presenceCollection")]
	public class Presence
	{
		[BsonId]
		[BsonGuidRepresentation(GuidRepresentation.Standard)]
		[BsonRepresentation(BsonType.String)]
		public Guid Id { get; set; }
		public DateTime DatePresence { get; set; }
		public Student Student { get; set; } = default!;
		public Teacher Teacher { get; set; } = default!;
		public Subject Subject { get; set; } = default!;

	}
}
