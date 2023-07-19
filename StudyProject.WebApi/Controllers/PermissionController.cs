using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using StudyProject.Application.ModelsDTO;
using StudyProject.Application.Services;
using StudyProject.Domain.Entities;
using StudyProject.Domain.Validation;

namespace StudyProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _permissionService;
        private readonly PermissionValidator _permissionValidator;

        public PermissionController(PermissionService permissionService, PermissionValidator permissionValidator)
        {
            _permissionService = permissionService;
            _permissionValidator = permissionValidator;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<PermissionDTO>> GetById(Guid id)
        {
            var result = await _permissionService.GetByIdAsync(id);

            if (result is null) return NoContent();

            return Ok(result);
        }

        [HttpGet("get-all/{page}/{count}")]
        public async Task<ActionResult<PermissionDTO>> GetAll(int page = 1, int count = 20)
        {
            var result = await _permissionService.GetAllAsync(page, count);

            if (result is null) return NoContent();

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<PermissionDTO>> Create([FromBody] PermissionDTO roleDTO)
        {
            var permission = roleDTO.Adapt<Permission>();
            var validation = await _permissionValidator.ValidateAsync(permission);

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Permission));
                return ValidationProblem(ModelState);
            }

            var result = await _permissionService.Create(permission);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<PermissionDTO>> Update([FromBody] PermissionDTO roleDTO)
        {
            var permission = roleDTO.Adapt<Permission>();
            var validation = await _permissionValidator.ValidateAsync(permission);

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Permission));
                return ValidationProblem(ModelState);
            }

            var result = await _permissionService.UpdateAsync(permission);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<PermissionDTO>> Delete(Guid id)
        {
            var result = await _permissionService.DeleteAsync(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
