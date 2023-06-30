using Mapster;
using MapsterMapper;
using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;

namespace StudyProject.Application.Services
{
    public class TestService
    {
        private readonly IMapper _mapper;
        public TestService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TenantDTO GetTenant(Tenant tenant)
        {
            return _mapper.From(tenant).AdaptToType<TenantDTO>();
        }
    }
}
