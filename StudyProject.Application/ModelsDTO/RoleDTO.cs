using StudyProject.Application.CommonDTO;

namespace StudyProject.Application.ModelsDTO
{
    public class RoleDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Permissions { get; set; }
    }
}
