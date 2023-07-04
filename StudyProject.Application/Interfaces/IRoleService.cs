using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.Interfaces
{
    public interface IRoleService
    {
        Task<RoleDTO> AddPermissionAsync(Guid permissionId, Guid roleId);
        Task<RoleDTO> Create(Role role);
        Task<RoleDTO> DeleteAsync(Guid id);
        Task<List<RoleDTO>> GetAllAsync(int page, int count);
        Task<RoleDTO> GetByIdAsync(Guid id);
        Task<RoleDTO> RemovePermissionAsync(Guid permissionId, Guid roleId);
        Task<RoleDTO> UpdateAsync(Role role);
    }
}