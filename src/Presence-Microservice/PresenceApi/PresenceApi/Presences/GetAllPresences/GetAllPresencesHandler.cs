using MongoDB.Bson;

namespace PresenceApi.Presences.GetAllPresences
{
	public record GetAllPresencesQuery(DateTime? Date, string? Subject, int? Limit, int? Page, string? Student	):IQuery<GetPresencesQueryResults>;
	public record GetPresencesQueryResults(IEnumerable<Presence> Presences, int Limit, int Page, long Registers);
	internal class GetAllPresencesQueryHandler(MongoDBContext _context, ILogger<GetAllPresencesQuery> logger) : IQueryHandler<GetAllPresencesQuery, GetPresencesQueryResults>
	{
		public async Task<GetPresencesQueryResults> Handle(GetAllPresencesQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Route GET ALL PRESENCES consulted with params {query}");


			int limit = query.Limit ?? 10;
			int page = query.Page ?? 0;	

			var filter = Builders<Presence>.Filter.Empty; 
			if(query.Date.HasValue)
			{
				var date = query.Date.Value.Date; 
				var startOfDay = date; 
				var endOfDay = date.AddDays(1).AddTicks(-1); 

				// Create a filter for the date
				filter = Builders<Presence>.Filter.And(
					Builders<Presence>.Filter.Gte(p => p.DatePresence, startOfDay),
					Builders<Presence>.Filter.Lte(p => p.DatePresence, endOfDay)
				);
			}
			if (query.Subject is not null)
			{
				var subjectFilter = Builders<Presence>.Filter.Eq(p => p.Subject.Id, Guid.Parse(query.Subject));
				filter = Builders<Presence>.Filter.And(filter, subjectFilter);
			}
			if(query.Student is not null)
			{
				var studentFilter = Builders<Presence>.Filter.Eq(p => p.Student.Id, Guid.Parse(query.Student));
				filter = Builders<Presence>.Filter.And(filter, studentFilter);
			}

			var listPresences = await _context.Presences.Find<Presence>(filter).Limit(limit).Skip((page *limit)).ToListAsync(cancellationToken);
			var totalRegister = await _context.Presences.CountDocumentsAsync(filter);
			return new GetPresencesQueryResults(listPresences, limit, page, totalRegister);
		}
	}
}
