using StudyProject.Domain.Common;

namespace StudyProject.Domain.Entities
{
    public class Permission : BaseEntity
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
