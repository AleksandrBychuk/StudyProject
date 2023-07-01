using StudyProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using StudyProject.Application.ModelsDTO;
using Mapster;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Validation;

namespace StudyProject.Application.Services
{
    public class TenantService
    {
        private readonly ApplicationDbContext _context;

        public TenantService(ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<TenantDTO> GetByIdAsync(Guid id)
        {
            var tenant = await _context.Tenants.AsNoTracking().Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == id);

            if (tenant == null)
                return null;

            return tenant.Adapt<TenantDTO>();
        }

        public async Task<List<TenantDTO>> GetAllAsync(int page, int count)
        {
            var skip = page == 1 ? 0 : count * page;
            var tenants = await _context.Tenants.AsNoTracking().Skip(skip).Take(count).ToListAsync();

            return tenants.Adapt<List<TenantDTO>>();
        }

        public async Task<TenantDTO> Create(Tenant tenant)
        {
            await _context.Tenants.AddAsync(tenant);
            await _context.SaveChangesAsync();

            return tenant.Adapt<TenantDTO>();
        }

        public async Task<TenantDTO> UpdateAsync(Tenant tenant)
        {
            var tenantToUpdate = await _context.Tenants.FindAsync(tenant.Id);

            if (tenantToUpdate == null)
                return null;

            tenantToUpdate.Name = tenant.Name;
            tenantToUpdate.Description = tenant.Description;

            _context.Tenants.Update(tenantToUpdate);
            await _context.SaveChangesAsync();

            return tenantToUpdate.Adapt<TenantDTO>();
        }

        public async Task<TenantDTO> DeleteAsync(Guid id)
        {
            var tenant = await _context.Tenants.FindAsync(id);

            if (tenant == null)
                return null;

            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();

            return tenant.Adapt<TenantDTO>();
        }

        public async Task<UserDTO> AddUserAsync(Guid tenantId, Guid userId)
        {
            var tenant = await _context.Tenants.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == tenantId);

            if (tenant == null)
                return null;

            var user = await _context.Users.Include(x => x.Emails).FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return null;

            if (tenant.Users.Any(x => x.Emails.Intersect(user.Emails).Count() > 0))
                return null;

            tenant.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.Adapt<UserDTO>();
        }

        public async Task<UserDTO> DeleteUserAsync(Guid tenantId, Guid userId)
        {
            var tenant = await _context.Tenants.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == tenantId);

            if (tenant == null)
                return null;

            var user = await _context.Users.FindAsync(userId);

            if (tenant == null)
                return null;

            tenant.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user.Adapt<UserDTO>();
        }
    }
}
