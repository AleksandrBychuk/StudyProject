using Mapster;
using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.Mapper
{
    public class RegisterMapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.RequireDestinationMemberSource = true;

            config.NewConfig<TenantDTO, Tenant>()
                .Ignore(x => x.Users)
                .Ignore(x => x.IsDeleted)
                .Ignore(x => x.DeletedAt)
                .Ignore(x => x.CreatedAt);
            config.NewConfig<Tenant, TenantDTO>()
                .Map(ud => ud.Users, a => a.Users == null ? Enumerable.Empty<string>() : a.Users.Select(x => x.FullName).ToList());

            config.NewConfig<UserDTO, User>()
               .Ignore(x => x.Emails)
               .Ignore(x => x.Tenants)
               .Ignore(x => x.IsDeleted)
               .Ignore(x => x.DeletedAt)
               .Ignore(x => x.CreatedAt);
            config.NewConfig<User, UserDTO>()
                .Map(ud => ud.RoleName, a => a.Role.Name)
                .Map(ud => ud.Emails, a => a.Emails == null ? Enumerable.Empty<string>() : a.Emails.Select(x => x.EmailAddress).ToList())
                .Map(ud => ud.Tenants, a => a.Tenants == null ? Enumerable.Empty<string>() : a.Tenants.Select(x => x.Name).ToList());

            config.NewConfig<Role, RoleDTO>()
                .Map(ud => ud.Permissions, a => a.Permissions.Select(x => x.Name).ToList());
        }
    }
}
