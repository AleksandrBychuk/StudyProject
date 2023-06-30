using StudyProject.Domain.Common;

namespace StudyProject.Domain.Entities
{
    public class Email : BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
