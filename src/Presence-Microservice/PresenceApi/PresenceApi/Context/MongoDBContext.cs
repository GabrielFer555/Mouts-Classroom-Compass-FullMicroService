using MongoDB.Driver;

namespace PresenceApi.Context
{
	public class MongoDBContext
	{
		private readonly IMongoDatabase _database;

		public MongoDBContext(IMongoClient client)
		{
			_database = client.GetDatabase("presence");
		}

		public IMongoCollection<Presence> Presences => _database.GetCollection<Presence>("presenceCollection");
	}
}
