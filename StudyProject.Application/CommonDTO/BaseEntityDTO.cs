namespace StudyProject.Application.CommonDTO
{
    public abstract class BaseEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
