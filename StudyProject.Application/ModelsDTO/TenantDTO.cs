using StudyProject.Application.CommonDTO;

namespace StudyProject.Application.ModelsDTO
{
    public class TenantDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserDTO> Users { get; set; } = new();
    }
}
