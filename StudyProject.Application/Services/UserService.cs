using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;
using StudyProject.Infrastructure.Persistence;

namespace StudyProject.Application.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.AsNoTracking().Include(x => x.Tenants).Include(x => x.Emails)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                return null;

            return user.Adapt<UserDTO>();
        }

        public async Task<List<UserDTO>> GetAllAsync(int page, int count)
        {
            var skip = page == 1 ? 0 : count * page;
            var users = await _context.Users.AsNoTracking().Skip(skip).Take(count).ToListAsync();

            return users.Adapt<List<UserDTO>>();
        }

        public async Task<UserDTO> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user.Adapt<UserDTO>();
        }

        public async Task<UserDTO> UpdateAsync(User user)
        {
            var userToUpdate = await _context.Users.FindAsync(user.Id);

            if (userToUpdate == null)
                return null;

            userToUpdate.FullName = user.FullName;
            userToUpdate.RoleId = user.RoleId;

            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();

            return userToUpdate.Adapt<UserDTO>();
        }

        public async Task<UserDTO> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user.Adapt<UserDTO>();
        }

        public async Task<UserDTO> AddEmailAsync(Email email, Guid userId)
        {
            var user = await _context.Users.Include(x => x.Emails).FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return null;

            if (user.Emails.Any(x => x.EmailAddress == email.EmailAddress))
                return null;

            user.Emails.Add(email);
            await _context.SaveChangesAsync();

            return user.Adapt<UserDTO>();
        }

        public async Task<UserDTO> RemoveEmailAsync(Guid emailId, Guid userId)
        {
            var user = await _context.Users.Include(x => x.Emails).FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return null;

            var email = user.Emails.FirstOrDefault(x => x.Id == emailId);

            if (email == null) return null;

            user.Emails.Remove(email);
            await _context.SaveChangesAsync();

            return user.Adapt<UserDTO>();
        }

        public async Task<UserDTO> SetRoleAsync(Guid roleId, Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);

            if (user == null || role == null)
                return null;

            user.Role = role;

            await _context.SaveChangesAsync();

            return user.Adapt<UserDTO>();
        }
    }
}
