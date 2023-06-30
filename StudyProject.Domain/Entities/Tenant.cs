using StudyProject.Domain.Common;
using StudyProject.Infrastructure.Interfaces;

namespace StudyProject.Domain.Entities
{
    public class Tenant : BaseEntity, ISoftDelete
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
}
