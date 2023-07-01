using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("get-all")]
        public async Task<ActionResult<TenantDTO>> GetAll(int page = 1, int count = 20)
        {
            var result = await _tenantService.GetAllAsync(page, count);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<TenantDTO>> Create([FromBody] TenantDTO tenantDto)
        {
            var tenant = tenantDto.Adapt<Tenant>();
            var validation = await _tenantValidator.ValidateAsync(tenant);

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Tenant));
                return ValidationProblem(ModelState);
            }


            var result = await _tenantService.Create(tenant);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<TenantDTO>> Update([FromBody] TenantDTO tenantDto)
        {
            var tenant = tenantDto.Adapt<Tenant>();
            var validation = await _tenantValidator.ValidateAsync(tenant);

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Tenant));
                return ValidationProblem(ModelState);
            }

            var result = await _tenantService.UpdateAsync(tenant);

            return Ok(result);
        }

        [HttpGet("delete/{id}")]
        public async Task<ActionResult<TenantDTO>> Delete(Guid id)
        {
            var result = await _tenantService.DeleteAsync(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("add-user")]
        public async Task<ActionResult<TenantDTO>> AddUser(Guid tenantId, Guid userId)
        {
            var result = await _tenantService.AddUserAsync(tenantId, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("delete-user")]
        public async Task<ActionResult<TenantDTO>> DeleteUser(Guid tenantId, Guid userId)
        {
            var result = await _tenantService.DeleteUserAsync(tenantId, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
