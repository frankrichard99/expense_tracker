using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ExpenseTracker.Services;
using ExpenseTracker.DTOs;


namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(UserService _usersService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var allUsers = await _usersService.GetAllUsers();

            List<UserResponseDto> response = allUsers.Select(u => new UserResponseDto {


                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Age = u.Age,

                Expenses = u.Expenses.Select(e => new ExpensePreviewDto
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    Date = e.Date



                }).ToList()
            }).ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
        {
            var user = await _usersService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            UserResponseDto response = new UserResponseDto
            {

                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,

                Expenses = user.Expenses.Select(e => new ExpensePreviewDto
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    Date = e.Date



                }).ToList()
            };
            return Ok(user);
        }
        /*
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> CreateUser(CreateUserDto userDto)
        {
           
            var newUser = await _usersService.AddUser(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Age) ;

            UserResponseDto response = new UserResponseDto
            {

                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Age = newUser.Age,

                Expenses = newUser.Expenses.Select(e => new ExpensePreviewDto
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    Date = e.Date



                }).ToList()
            };
            return CreatedAtAction(nameof(GetUserById), new { id = response.Id }, response);
        }
        */
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, UpdateUserDto user)
        {

            var updatedUser = await _usersService.UpdateUser(id, user.FirstName, user.LastName, user.Email, user.Age);

            if(updatedUser == null)
            {
                return BadRequest("User Not Updated");
            }

            UserResponseDto response = new UserResponseDto
            {
                Id = updatedUser.Id,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Email = updatedUser.Email,
                Age = updatedUser.Age,
                Expenses = updatedUser.Expenses.Select(e => new ExpensePreviewDto
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    Date = e.Date



                }).ToList()

            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var success = await _usersService.DeleteUser(id);
            if (!success)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
