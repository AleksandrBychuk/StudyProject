using FluentValidation;
using StudyProject.Domain.Entities;

namespace StudyProject.Domain.Validation
{
    public class PermissionValidator : AbstractValidator<Permission>
    {
        public PermissionValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Name).Length(0, 10).NotNull().NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
            RuleFor(x => x.PermissionType).NotNull();
        }
    }
}
