using Microsoft.EntityFrameworkCore;
using StudyProject.Domain.Entities;
using StudyProject.Infrastructure.Interfaces;

namespace StudyProject.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Tenant> Tenants => Set<Tenant>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();

        public override int SaveChanges()
        {
            SoftDelete();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SoftDelete();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ISoftDelete entity)
                {
                    if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        entity.IsDeleted = true;
                        entity.DeletedAt = DateTimeOffset.UtcNow;
                    }
                }
            }
        }
    }
}
