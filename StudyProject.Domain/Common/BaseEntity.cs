using Mapster;

namespace StudyProject.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [AdaptIgnore]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        [AdaptIgnore]
        public DateTimeOffset DeletedAt { get; set; }

        [AdaptIgnore]
        public bool IsDeleted { get; set; } = false;
    }
}
