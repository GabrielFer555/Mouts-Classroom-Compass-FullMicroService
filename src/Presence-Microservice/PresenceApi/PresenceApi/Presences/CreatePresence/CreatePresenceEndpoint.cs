
using FluentValidation.Results;
using PresenceApi.Models;
using PresenceApi.Validators;

namespace PresenceApi.Presences.CreatePresence
{
	public record CreatePresenceRequest(DateTime DatePresence, Student Student,Teacher Teacher, Subject Subject);
	public record CreatePresenceResponse(Guid Id);

	public class CreatePresenceEndpoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/presence", async (CreatePresenceRequest request, ISender sender, PresenceValidator validator) =>
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


					var command = request.Adapt<CreatePresenceCommand>();

					var result = await sender.Send(command);
					var response = result.Adapt<CreatePresenceResponse>();
					return Results.Created($"/presence/{response.Id}", response);
				}
				catch (Exception ex) { 
					return Results.BadRequest(ex);
				}

			});
		}
	}
}
