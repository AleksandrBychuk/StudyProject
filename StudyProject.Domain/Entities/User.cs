using StudyProject.Domain.Common;
using StudyProject.Infrastructure.Interfaces;

namespace StudyProject.Domain.Entities
{
    public class User : BaseEntity, ISoftDelete
    {
        public string FullName { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public List<Email> Emails { get; set; }
        public List<Tenant> Tenants { get; set; }
    }
}
