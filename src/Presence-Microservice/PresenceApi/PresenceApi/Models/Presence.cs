namespace PresenceApi.Models
{
	public class Presence
	{
		public Guid Id { get; set; }
		public DateTime DatePresence { get; set; }
		public Student Student { get; set; } = default!;
		public Teacher Teacher { get; set; } = default!;
		public Subject Subject { get; set; } = default!;

	}
}
