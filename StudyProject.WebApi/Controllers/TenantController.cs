using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Application.Interfaces;
using StudyProject.Application.ModelsDTO;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Validation;

namespace StudyProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly TenantValidator _tenantValidator;

        public TenantController(ITenantService tenantService, TenantValidator tenantValidator)
        {
            _tenantService = tenantService;
            _tenantValidator = tenantValidator;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<TenantDTO>> GetById(Guid id)
        {
            var result = await _tenantService.GetByIdAsync(id);

            if (result is null) return NoContent();

            return Ok(result);
        }

        [HttpGet("get-all/{page}/{count}")]
        public async Task<ActionResult<TenantDTO>> GetAll(int page = 1, int count = 20)
        {
            var result = await _tenantService.GetAllAsync(page, count);

            if (result is null) return NoContent();

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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<TenantDTO>> Delete(Guid id)
        {
            var result = await _tenantService.DeleteAsync(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("add-user")]
        public async Task<ActionResult<TenantDTO>> AddUser([FromQuery] Guid tenantId, [FromQuery] Guid userId)
        {
            var result = await _tenantService.AddUserAsync(tenantId, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("delete-user")]
        public async Task<ActionResult<TenantDTO>> DeleteUser([FromQuery] Guid tenantId, [FromQuery] Guid userId)
        {
            var result = await _tenantService.RemoveUserAsync(tenantId, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
