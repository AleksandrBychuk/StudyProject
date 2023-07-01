using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Application.ModelsDTO;
using StudyProject.Application.Services;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Validation;

namespace StudyProject.WebApi.Controllers
{
    [Controller]
    [Route("Tenant")]
    public class TenantController : ControllerBase
    {
        private readonly TenantService _tenantService;
        private readonly TenantValidator _tenantValidator;

        public TenantController(TenantService tenantService, TenantValidator tenantValidator)
        {
            _tenantService = tenantService;
            _tenantValidator = tenantValidator;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<TenantDTO>> GetById(Guid id)
        {
            var result = await _tenantService.GetByIdAsync(id);

            return Ok(result);
        }

        //public async Task<ActionResult<TenantDTO>> GetAll(int page = 1, int count = 20)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpPost("Add")]
        public async Task<ActionResult<TenantDTO>> Create([FromBody] TenantDTO tenant)
        {
            var tenantNoDTO = tenant.Adapt<Tenant>();
            var validation = await _tenantValidator.ValidateAsync(tenant.Adapt<Tenant>());

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Tenant));

                return ValidationProblem(ModelState);
            }


            var result = await _tenantService.Create(tenant.Adapt<Tenant>());

            return Ok(result);
        }

        //public async Task<ActionResult<TenantDTO>> Update(Tenant tenant)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet("delete/{id}")]
        //public async Task<ActionResult<TenantDTO>> Delete(Guid id)
        //{
        //    var result = await _tenantService.DeleteAsync(id);

        //    if(result == null)
        //        return NoContent();

        //    return result;
        //}

        //public async Task<ActionResult<TenantDTO>> AddUser(Guid tenantId, Guid userId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<ActionResult<TenantDTO>> DeleteUser(Guid tenantId, Guid userId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
