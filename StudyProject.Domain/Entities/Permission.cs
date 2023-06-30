using StudyProject.Domain.Common;
using StudyProject.Infrastructure.Interfaces;

namespace StudyProject.Domain.Entities
{
    public class Permission : BaseEntity, ISoftDelete
    {
        public string Name { get; set; }
        public PermissionType PermissionType { get; set; } = PermissionType.Nothing;
        public List<Role> Roles { get; set; }
    }

    public enum PermissionType
    {
        All = 0,
        OnlyRead = 1,
        Nothing = 2
    }
}
