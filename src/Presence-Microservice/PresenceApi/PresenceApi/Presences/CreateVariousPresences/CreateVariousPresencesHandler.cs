

namespace PresenceApi.Presences.CreateVariousPresences
{
	public record CreateVariousPresencesCommand(List<Presence> Presences):ICommand<CreateVariousPresencesCommandResult>;
	public record CreateVariousPresencesCommandResult(List<Guid> PresencesId);
	internal class CreateVariousPresencesHandler(MongoDBContext _context, ILogger<List<Presence>> _logger) : ICommandHandler<CreateVariousPresencesCommand, CreateVariousPresencesCommandResult>
	{
		public async Task<CreateVariousPresencesCommandResult> Handle(CreateVariousPresencesCommand command, CancellationToken cancellationToken)
		{

			await _context.Presences.InsertManyAsync(command.Presences,null, cancellationToken);
			List<Guid> presencesId = new();
			foreach(var presence in command.Presences)
			{
				presencesId.Add(presence.Id);
			}
			return new CreateVariousPresencesCommandResult(presencesId);
		}
	}
}
