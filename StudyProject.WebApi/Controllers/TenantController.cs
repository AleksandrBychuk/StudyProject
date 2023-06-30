using Microsoft.AspNetCore.Mvc;
using StudyProject.Application.ModelsDTO;
using StudyProject.Application.Services;
using StudyProject.Domain.Entities;
using StudyProject.Infrastructure.Persistence;

namespace StudyProject.WebApi.Controllers
{
    [Controller]
    [Route("Tenant")]
    public class TenantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TestService _test;
        public TenantController(ApplicationDbContext context, TestService test)
        {
            _context = context;
            _test = test;
        }

        [HttpGet("test")]
        public ActionResult<TenantDTO> Index()
        {
            return _test.GetTenant(new Tenant { Name = "test", Description = "test" });
        }
    }
}
