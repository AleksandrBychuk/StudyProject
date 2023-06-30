using FluentValidation;
using StudyProject.Domain.Entities;

namespace StudyProject.Domain.Validation
{
    public class TenantValidator : AbstractValidator<Tenant>
    {
        public TenantValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 20).NotNull().NotEmpty();
            RuleFor(x => x.Description).NotEmpty().Length(0, 200);
            RuleFor(x => x.IsDeleted).NotEqual(true);
            RuleFor(x => x.Users.GroupBy(x => x.Emails).Any(x => x.Count() > 1)).NotEqual(true);
        }
    }
}
