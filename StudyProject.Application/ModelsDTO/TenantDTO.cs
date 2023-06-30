using StudyProject.Domain.Entities;

namespace StudyProject.Application.ModelsDTO
{
    public class TenantDTO
    {
        public string Name { get; set; }
        public List<string> Users { get; set; }
    }
}
