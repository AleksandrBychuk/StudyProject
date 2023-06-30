using StudyProject.Domain.Entities;

namespace StudyProject.Application.ModelsDTO
{
    internal class UserDTO
    {
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Tenants { get; set; }
    }
}
