using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.Interfaces
{
    public interface ITenantService
    {
        Task<UserDTO> AddUserAsync(Guid tenantId, Guid userId);
        Task<TenantDTO> Create(Tenant tenant);
        Task<TenantDTO> DeleteAsync(Guid id);
        Task<List<TenantDTO>> GetAllAsync(int page, int count);
        Task<TenantDTO> GetByIdAsync(Guid id);
        Task<UserDTO> RemoveUserAsync(Guid tenantId, Guid userId);
        Task<TenantDTO> UpdateAsync(Tenant tenant);
    }
}