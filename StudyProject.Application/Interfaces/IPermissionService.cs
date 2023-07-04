using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<PermissionDTO> Create(Permission permission);
        Task<PermissionDTO> DeleteAsync(Guid id);
        Task<List<PermissionDTO>> GetAllAsync(int page, int count);
        Task<PermissionDTO> GetByIdAsync(Guid id);
        Task<PermissionDTO> UpdateAsync(Permission permission);
    }
}