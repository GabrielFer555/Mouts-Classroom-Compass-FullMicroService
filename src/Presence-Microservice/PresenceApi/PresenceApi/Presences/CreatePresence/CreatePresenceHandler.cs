
using Newtonsoft.Json;
using System.Text.Json;

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


			string urlAuth = "https://mcc:8081/Login";

			UserAuthBodyRequest data = new UserAuthBodyRequest("gaybriel@example.com", "Gabriel2004!!");

			var httpRequest = new HttpClient(new HttpClientHandler
			{
				ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
			});
			var jsonText =  JsonConvert.SerializeObject(data);
			var content = new StringContent(jsonText, System.Text.Encoding.UTF8, "application/json");
			var responseAuth = await httpRequest.PostAsync(urlAuth, content, cancellationToken: cancellationToken);
			if (!responseAuth.IsSuccessStatusCode) {
				throw new Exception("Impossible to validate data");
			}
			string token = await responseAuth.Content.ReadAsStringAsync();
			string validationUrl = $"https://mcc:8081/api/Validation?subject={command.Subject.Id}&teacher={command.Teacher.Id}&student={command.Student.Id}";
           

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
