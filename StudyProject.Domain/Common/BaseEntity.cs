namespace StudyProject.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
