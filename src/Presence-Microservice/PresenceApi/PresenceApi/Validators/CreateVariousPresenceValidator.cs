using PresenceApi.Presences.CreateVariousPresences;

namespace PresenceApi.Validators
{
	public class CreateVariousPresenceValidator:AbstractValidator<CreateVariousPresencesRequest>
	{

		public CreateVariousPresenceValidator() {
			RuleFor(x => x.Presences).Must(items => items != null && items.Count >= 1).WithMessage("List of presences must contain at least one item").ForEach(e => e.ChildRules(item =>
			{
				item.RuleFor(ex => ex.DatePresence).NotNull().NotEmpty().LessThanOrEqualTo(DateTime.Now);
			}));
		}
	}
}
