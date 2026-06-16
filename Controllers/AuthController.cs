using ExpenseTracker.DTOs;
using ExpenseTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class AuthController(AuthService _authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(RegisterDto registerDto)
        {
            var user = await _authService.Register(registerDto);

            if (user == null)
            {
                return BadRequest("Email exists already.");
            }

            var response = new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age
            };

            return CreatedAtAction(
                "GetUserById",
                "Users",
                new { id = response.Id },
                response
            );
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDto>> Login(LoginDto loginDto)
        {
            var user = await _authService.Login(loginDto);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            
            var response = new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age
            };

            return Ok(response);
        }
    }
}