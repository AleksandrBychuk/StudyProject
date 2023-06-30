using FluentValidation;
using StudyProject.Domain.Entities;

namespace StudyProject.Domain.Validation
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 50).NotNull().NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}
