
using PresenceApi.Models;

namespace PresenceApi.Presences.GetAllPresences
{	public record GetProductResponse(IEnumerable<Presence> Presences);
	public class GetAllPresencesEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/presence", async (ISender sender, DateTime? datePresence, string? subject) =>
			{
				
				var result = await sender.Send(new GetAllPresencesQuery(datePresence, subject));
				var response = result.Adapt<GetProductResponse>();

				return Results.Ok(response);
			});
		}
	}
}
