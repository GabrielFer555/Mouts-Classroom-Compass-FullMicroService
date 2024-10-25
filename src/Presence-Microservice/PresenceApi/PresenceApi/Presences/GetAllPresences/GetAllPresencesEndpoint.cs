
using PresenceApi.Models;

namespace PresenceApi.Presences.GetAllPresences
{	public record GetProductResponse(IEnumerable<Presence> Presences, int Limit, int Page, long Registers);
	public class GetAllPresencesEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/presence", async (ISender sender, DateTime? datePresence, string? subject, int? limit, int? page) =>
			{
				
				var result = await sender.Send(new GetAllPresencesQuery(datePresence, subject, limit, page));
				var response = result.Adapt<GetProductResponse>();

				return Results.Ok(response);
			});
		}
	}
}
