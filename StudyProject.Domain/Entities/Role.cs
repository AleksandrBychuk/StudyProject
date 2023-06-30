using StudyProject.Domain.Common;

namespace StudyProject.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PermissionId { get; set; }
        public List<Permission> Permission { get; set; }
    }
}
