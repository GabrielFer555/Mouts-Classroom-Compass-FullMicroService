namespace PresenceApi.Models
{
	public record UserAuthBodyRequest(string Username, string Password);
	public record UserAuthBodyResponse(string Token);
	public record UserValidationBody(string StudentName, string TeacherName, string SubjectName);
}
