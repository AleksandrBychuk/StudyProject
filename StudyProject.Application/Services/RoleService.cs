using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyProject.Application.Interfaces;
using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;
using StudyProject.Infrastructure.Persistence;

namespace StudyProject.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RoleDTO> GetByIdAsync(Guid id)
        {
            var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (role == null) return null;

            return role.Adapt<RoleDTO>();
        }

        public async Task<List<RoleDTO>> GetAllAsync(int page, int count)
        {
            var skip = page == 1 ? 0 : count * page;
            var role = await _context.Roles.AsNoTracking().Skip(skip).Take(count).ToListAsync();

            return role.Adapt<List<RoleDTO>>();
        }

        public async Task<RoleDTO> Create(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return role.Adapt<RoleDTO>();
        }

        public async Task<RoleDTO> UpdateAsync(Role role)
        {
            var roleToUpdate = await _context.Roles.FindAsync(role.Id);

            if (roleToUpdate == null) return null;

            roleToUpdate.Name = role.Name;
            roleToUpdate.Description = role.Description;

            _context.Roles.Update(roleToUpdate);
            await _context.SaveChangesAsync();

            return roleToUpdate.Adapt<RoleDTO>();
        }

        public async Task<RoleDTO> DeleteAsync(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null) return null;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return role.Adapt<RoleDTO>();
        }

        public async Task<RoleDTO> AddPermissionAsync(Guid permissionId, Guid roleId)
        {
            var role = await _context.Roles.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == roleId);
            var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.Id == permissionId);

            if (role == null || permission == null) return null;

            if (role.Permissions.Any(x => x.Id == permission.Id)) return null;

            role.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return role.Adapt<RoleDTO>();
        }

        public async Task<RoleDTO> RemovePermissionAsync(Guid permissionId, Guid roleId)
        {
            var role = await _context.Roles.Include(x => x.Permissions).FirstOrDefaultAsync(x => x.Id == roleId);

            if (role == null) return null;

            if (!role.Permissions.Any(x => x.Id == permissionId)) return null;

            role.Permissions.RemoveAll(x => x.Id == permissionId);
            await _context.SaveChangesAsync();

            return role.Adapt<RoleDTO>();
        }
    }
}
