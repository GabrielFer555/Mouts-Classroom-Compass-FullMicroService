
using PresenceApi.Validators;

namespace PresenceApi.Presences.CreateVariousPresences
{

	public record CreateVariousPresencesRequest(List<Presence> Presences);
	public record CreateVariousPresencesResponse(List<Guid> PresencesId);
	public class CreateVariousPresencesEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/presence/various", async (CreateVariousPresencesRequest request, ISender sender, CreateVariousPresenceValidator validator) =>
			{
				try
				{
					var isValid = validator.Validate(request);


					if (!isValid.IsValid)
					{
						List<string> errorMessages = new();
						foreach (var errors in isValid.Errors)
						{
							errorMessages.Add(errors.ErrorMessage);
						}
						return Results.BadRequest(errorMessages);
					}
					var result = await sender.Send(new CreateVariousPresencesCommand(request.Presences));

					var response = result.Adapt<CreateVariousPresencesResponse>();
					return Results.Created("/presence/various",response);
				}
				catch (Exception ex) {
					return Results.BadRequest(ex.Message);
				}
			});
		}
	}
}
