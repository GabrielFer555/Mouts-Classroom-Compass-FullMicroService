using FluentValidation;
using PresenceApi.Presences.CreatePresence;

namespace PresenceApi.Validators
{
	public class PresenceValidator:AbstractValidator<CreatePresenceRequest>
	{
		public PresenceValidator() { 
			RuleFor(x=> x.DatePresence).NotNull().NotEmpty().LessThanOrEqualTo(DateTime.Now);

			RuleFor(x => x.Subject.Id).NotNull().NotEmpty();

			RuleFor(x => x.Subject).NotNull().NotEmpty();

			RuleFor(x => x.Teacher.Id).NotNull().NotEmpty();

			RuleFor(x => x.Teacher).NotNull().NotEmpty();

			RuleFor(x => x.Student.Id).NotNull().NotEmpty();

			RuleFor(x => x.Student).NotNull().NotEmpty();
		}
	}
}
