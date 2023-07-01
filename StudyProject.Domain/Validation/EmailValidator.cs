using FluentValidation;
using StudyProject.Domain.Entities;

namespace StudyProject.Domain.Validation
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.EmailAddress).Length(0, 50).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}
