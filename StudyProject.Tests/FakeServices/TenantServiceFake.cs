using Mapster;
using Microsoft.EntityFrameworkCore;
using StudyProject.Application.Interfaces;
using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;
using StudyProject.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProject.Application.Services
{
    public class TenantServiceFake : ITenantService
    {
        private readonly List<TenantDTO> _tenants;

        public TenantServiceFake()
        {
            _tenants = new()
            {
                new TenantDTO { Id = Guid.Parse("bafb71ba-e350-40b7-b544-cc0eb04c6ea7"), Name = "Test1", Description = "Test1", 
                    Users = new List<string> { "User1", "User2" } },
                new TenantDTO { Id = Guid.Parse("dd25f694-2984-4327-b2ec-5651fe9a9c8d"), Name = "Test2", Description = "Test2"},
                new TenantDTO { Id = Guid.Parse("b6640af7-6c20-41bf-b874-7364522edf75"), Name = "Test3", Description = "Test3"},
                new TenantDTO { Id = Guid.Parse("14c3078b-fa7c-432c-a329-f386fdd48a6a"), Name = "Test4", Description = "Test4"},
            };
        }

        public async Task<TenantDTO> GetByIdAsync(Guid id)
        {
            var tenant = _tenants.FirstOrDefault(x => x.Id == id);

            if (tenant == null) return null;

            return tenant;
        }

        public async Task<List<TenantDTO>> GetAllAsync(int page, int count)
        {
            var skip = page == 1 ? 0 : count * page;
            var tenants = _tenants.Skip(skip).Take(count).ToList();

            return tenants;
        }

        public async Task<TenantDTO> Create(TenantDTO tenant)
        {
            tenant.Id = Guid.NewGuid();
            _tenants.Add(tenant);
            
            return tenant;
        }

        public async Task<TenantDTO> UpdateAsync(TenantDTO tenant)
        {
            var tenantToUpdate = _tenants.Find(x => x.Id == tenant.Id);

            if (tenantToUpdate == null)
                return null;

            tenantToUpdate.Name = tenant.Name;
            tenantToUpdate.Description = tenant.Description;

            return tenantToUpdate;
        }

        public async Task<TenantDTO> DeleteAsync(Guid id)
        {
            var tenant = _tenants.Find(x => x.Id == id);

            if (tenant == null)
                return null;

            _tenants.Remove(tenant);

            return tenant;
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

        public async Task<UserDTO> RemoveUserAsync(Guid tenantId, Guid userId)
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
