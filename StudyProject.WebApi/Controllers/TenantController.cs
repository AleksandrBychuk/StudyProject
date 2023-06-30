using Microsoft.AspNetCore.Mvc;
using StudyProject.Domain.Entities;
using StudyProject.Infrastructure.Persistence;

namespace StudyProject.WebApi.Controllers
{
    [Controller]
    [Route("Tenant")]
    public class TenantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TenantController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public ActionResult<string> Index()
        {
            _context.Tenants.Add(new Tenant { Name = "test", Description = "test" });
            _context.SaveChanges();
            return "Hello";
        }
    }
}
