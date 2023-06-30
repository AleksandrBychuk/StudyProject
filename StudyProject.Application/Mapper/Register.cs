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
            config.NewConfig<Tenant, TenantDTO>()
                .Map(ud => ud.Users, a => a.Users.Select(x => x.FullName).ToList());
            config.NewConfig<User, UserDTO>()
                .Map(ud => ud.RoleName, a => a.Role.Name)
                .Map(ud => ud.Emails, a => a.Emails.Select(x => x.Name).ToList())
                .Map(ud => ud.Tenants, a => a.Tenants.Select(x => x.Name).ToList());
            config.NewConfig<Role, RoleDTO>()
                .Map(ud => ud.Permissions, a => a.Permissions.Select(x => x.Name).ToList());
        }
    }
}
