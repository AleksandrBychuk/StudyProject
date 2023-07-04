using StudyProject.Application.CommonDTO;

namespace StudyProject.Application.ModelsDTO
{
    public class UserDTO : BaseEntityDTO
    {
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Tenants { get; set; }
    }
}
