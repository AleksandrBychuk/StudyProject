using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;
using StudyProject.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.Application.Services
{
    public class RoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RoleDTO> GetByIdAsync(Guid id)
        {
            var role = await _context.Roles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);

            if (role == null) return null;

            return role.Adapt<RoleDTO>();
        }

        public async Task<List<RoleDTO>> GetAllAsync(int page, int count)
        {
            var skip = page == 1 ? 0 : count * page;
            var role = await _context.Roles.AsNoTracking().Where(x => x.IsDeleted == false).Skip(skip).Take(count).ToListAsync();

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
            var user = await _context.Users.FindAsync(id);

            if (user == null) return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user.Adapt<RoleDTO>();
        }
    }
}
