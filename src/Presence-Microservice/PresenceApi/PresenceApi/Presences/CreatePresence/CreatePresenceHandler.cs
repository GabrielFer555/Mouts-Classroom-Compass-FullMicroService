using BuildingBlocks.CQRS;
using PresenceApi.Context;
using PresenceApi.Models;

namespace PresenceApi.Presences.CreatePresence
{
	public record CreatePresenceCommand(DateTime DatePresence, Student Student, Teacher Teacher, Subject Subject):ICommand<CreatePresenceResult>;
	public record CreatePresenceResult(Guid Id);
	public class CreatePresenceHandler(MongoDBContext _context) : ICommandHandler<CreatePresenceCommand, CreatePresenceResult>
	{
		public async Task<CreatePresenceResult> Handle(CreatePresenceCommand command, CancellationToken cancellationToken)
		{
			Presence presence = new Presence
			{
				DatePresence = command.DatePresence,
				Student = command.Student,
				Subject = command.Subject,
				Teacher= command.Teacher,
			};

			await _context.Presences.InsertOneAsync(presence, cancellationToken: cancellationToken);

			return new CreatePresenceResult(presence.Id);
		}
	}
}
