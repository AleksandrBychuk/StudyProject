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
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly UserValidator _userValidator;

        public UserController(UserService userService, UserValidator userValidator)
        {
            _userService = userService;
            _userValidator = userValidator;
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var result = await _userService.GetByIdAsync(id);

            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<UserDTO>> GetAll([FromQuery] int page = 1, [FromQuery] int count = 20)
        {
            var result = await _userService.GetAllAsync(page, count);

            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<ActionResult<UserDTO>> Create([FromBody] UserDTO userDTO)
        {
            var user = userDTO.Adapt<User>();
            var validation = await _userValidator.ValidateAsync(user);

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Domain.Entities.User));
                return ValidationProblem(ModelState);
            }

            var result = await _userService.Create(user);

            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult<UserDTO>> Update([FromBody] UserDTO userDto)
        {
            var user = userDto.Adapt<User>();
            var validation = await _userValidator.ValidateAsync(user);

            if (!validation.IsValid)
            {
                validation.AddToModelState(ModelState, nameof(Domain.Entities.User));
                return ValidationProblem(ModelState);
            }

            var result = await _userService.UpdateAsync(user);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<UserDTO>> Delete(Guid id)
        {
            var result = await _userService.DeleteAsync(id);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpPost("add-email")]
        public async Task<ActionResult<UserDTO>> AddEmail([FromBody] EmailDTO emailDTO, [FromQuery] Guid userId)
        {
            var email = emailDTO.Adapt<Email>();
            var result = await _userService.AddEmailAsync(email, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }

        [HttpDelete("remove-email")]
        public async Task<ActionResult<UserDTO>> RemoveEmail([FromQuery] Guid emailId, [FromQuery] Guid userId)
        {
            var result = await _userService.RemoveEmailAsync(emailId, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }


        [HttpPut("set-role")]
        public async Task<ActionResult<UserDTO>> SetRole([FromQuery] Guid roleId, [FromQuery] Guid userId)
        {
            var result = await _userService.SetRoleAsync(roleId, userId);

            if (result == null)
                return BadRequest();

            return Ok(result);
        }
    }
}
