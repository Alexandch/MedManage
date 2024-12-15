using MedManage.Application.DTOs;
using MedManage.Application.Interfaces;
using MedManage.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedManage.Application.Filters;

namespace MedManage.WebAPI.Controllers
{
    [ApiController]
    [ValidateModelState] 
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        
        
        [HttpGet("all/{userId}")]
        public async Task<IActionResult> GetAllUsersExceptAsync(Guid userId)
        {
            var users = await _userService.GetAllUsersExceptAsync(userId);
            return Ok(users);
        }

        // Обновить информацию о пользователе
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUserInfoAsync([FromBody] UserDTO updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest(new { message = "User data is required." });
            }

            try
            {
                await _userService.UpdateUserInfoAsync(updatedUser);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // Получить имя пользователя из токена
        [HttpGet("name")]
        public IActionResult GetUserNameFromToken()
        {
            try
            {
                var userName = _userService.GetUserNameFromToken();
                return Ok(new { userName });
            }
            catch (InvalidOperationException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
