using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> AddEmailAsync(Email email, Guid userId);
        Task<UserDTO> Create(User user);
        Task<UserDTO> DeleteAsync(Guid id);
        Task<List<UserDTO>> GetAllAsync(int page, int count);
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<UserDTO> RemoveEmailAsync(Guid emailId, Guid userId);
        Task<UserDTO> SetRoleAsync(Guid roleId, Guid userId);
        Task<UserDTO> UpdateAsync(User user);
    }
}