using FluentValidation;
using StudyProject.Domain.Entities;

namespace StudyProject.Domain.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.FullName).Length(0, 50).NotNull().NotEmpty();
            RuleFor(x => x.Emails.Any(x => (string.IsNullOrEmpty(x.Name) || string.IsNullOrWhiteSpace(x.Name))
                || x.Name.Length > 200)).NotEqual(true);
            RuleFor(x => x.IsDeleted).NotEqual(true);
            RuleFor(x => x.Emails.GroupBy(x => x).Any(x => x.Count() > 1)).NotEqual(true);
            RuleFor(x => x.RoleId).NotNull();
        }
    }
}
