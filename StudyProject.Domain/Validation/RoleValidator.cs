using FluentValidation;
using StudyProject.Domain.Entities;

namespace StudyProject.Domain.Validation
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 10).NotNull().NotEmpty();
            RuleFor(x => x.Description).Length(0, 200).NotNull().NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
            RuleFor(x => x.Permissions.Count).NotEqual(0);
        }
    }
}
