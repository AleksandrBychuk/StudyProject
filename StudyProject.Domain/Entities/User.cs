using StudyProject.Domain.Common;

namespace StudyProject.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public List<Email> Email { get; set; }
        public List<Tenant> Tenants { get; set; }
    }
}
