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
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        private readonly RoleValidator _roleValidator;

        public RoleController(RoleService roleService, RoleValidator roleValidator)
        {
            _roleService = roleService;
            _roleValidator = roleValidator;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<RoleDTO>> GetById(Guid id)
        {
            var result = await _roleService.GetByIdAsync(id);

            if (result is null) return NoContent();

            return Ok(result);
        }

        [HttpGet("get-all/{page}/{count}")]
        public async Task<ActionResult<RoleDTO>> GetAll(int page = 1, int count = 20)
        {
            var result = await _roleService.GetAllAsync(page, count);

            if (result is null) return NoContent();

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<RoleDTO>> Create([FromBody] RoleDTO roleDTO)
        {
            var role = roleDTO.Adapt<Role>();
            var validation = await _roleValidator.ValidateAsync(role);

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Role));
                return ValidationProblem(ModelState);
            }

            var result = await _roleService.Create(role);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<RoleDTO>> Update([FromBody] RoleDTO roleDTO)
        {
            var role = roleDTO.Adapt<Role>();
            var validation = await _roleValidator.ValidateAsync(role);

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Role));
                return ValidationProblem(ModelState);
            }

            var result = await _roleService.UpdateAsync(role);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<RoleDTO>> Delete(Guid id)
        {
            var result = await _roleService.DeleteAsync(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("add-permission")]
        public async Task<ActionResult<RoleDTO>> AddEmail([FromBody] Guid permissionId, [FromQuery] Guid userId)
        {
            var result = await _roleService.AddPermissionAsync(permissionId, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("remove-permission")]
        public async Task<ActionResult<RoleDTO>> RemoveEmail([FromQuery] Guid permissionId, [FromQuery] Guid userId)
        {
            var result = await _roleService.RemovePermissionAsync(permissionId, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
