using StudyProject.Application.CommonDTO;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.ModelsDTO
{
    public class PermissionDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public PermissionType PermissionType { get; set; }
    }
}
