using System.Text.Json;
using BuildingBlocks.CQRS;
using PresenceApi.Context;
using PresenceApi.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Net.Http;


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


			string urlAuth = "https://localhost:7172/Login";
			UserAuthBodyRequest data = new UserAuthBodyRequest("gaybriel@example.com", "Gabriel2004!!");

			var httpRequest = new HttpClient();
			var responseAuth = await httpRequest.PostAsJsonAsync(urlAuth, data);
			if (!responseAuth.IsSuccessStatusCode) {
				throw new Exception("Impossible to validate data");
			}
			string token = await responseAuth.Content.ReadAsStringAsync();

			string validationUrl = $"https://localhost:7172/api/Validation?subject={command.Subject.Id}&teacher={command.Teacher.Id}&student={command.Student.Id}";


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


			await _context.Presences.InsertOneAsync(presence, cancellationToken: cancellationToken);

			return new CreatePresenceResult(presence.Id);
		}
	}
}
