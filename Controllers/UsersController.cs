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
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(_usersService.GetUsers());
        }
        [HttpPost]
        public ActionResult<User> CreateUser(CreateUserDto user)
        {
           
            var newUser = new User
            {
                Id = ExpenseTracker.Models.User.NextId(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Age = user.Age,
            };
            _usersService.AddUser(newUser);
            return CreatedAtAction(nameof(GetAllUsers), new { id = newUser.Id }, user);
        }

    }
}
