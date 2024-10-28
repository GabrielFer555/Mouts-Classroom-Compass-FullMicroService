

using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PresenceApi.Presences.CreateVariousPresences
{
	public record CreateVariousPresencesCommand(List<Presence> Presences):ICommand<CreateVariousPresencesCommandResult>;
	public record CreateVariousPresencesCommandResult(List<Guid> PresencesId);
	internal class CreateVariousPresencesHandler(MongoDBContext _context, ILogger<List<Presence>> _logger) : ICommandHandler<CreateVariousPresencesCommand, CreateVariousPresencesCommandResult>
	{
		public async Task<CreateVariousPresencesCommandResult> Handle(CreateVariousPresencesCommand command, CancellationToken cancellationToken)
		{

			string urlAuth = "https://localhost:7172/Login";
			UserAuthBodyRequest data = new UserAuthBodyRequest("gaybriel@example.com", "Gabriel2004!!");
			var httpRequest = new HttpClient();
			var responseAuth = await httpRequest.PostAsJsonAsync(urlAuth, data);
			if (!responseAuth.IsSuccessStatusCode)
			{
				throw new Exception("Impossible to validate data");
			}
			string token = await responseAuth.Content.ReadAsStringAsync();

			foreach(var presence in command.Presences)
			{
				string validationUrl = $"https://localhost:7172/api/Validation?subject={presence.Subject.Id}&teacher={presence.Teacher.Id}&student={presence.Student.Id}";


				httpRequest.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
				var response = await httpRequest.GetAsync(validationUrl);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("Impossible to validate data");
			}
				string responseBodyString = await response.Content.ReadAsStringAsync();
				var responseBody = JsonConvert.DeserializeObject<UserValidationBody>(responseBodyString);

				presence.Student.Name = responseBody.StudentName;
				presence.Subject.Description = responseBody.SubjectName;
				presence.Teacher.Name = responseBody.TeacherName;
			}


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
