using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace ExpenseTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        static private List<User> users = new List<User> { new User { Id = "1", FirstName = "frank", LastName = "richard", Age = 19 }, new User { Id = "2", FirstName = "chibuike", LastName = "john", Age = 23 }, new User { Id = "3", FirstName = "benny", LastName = "kiur", Age = 13 } };


        [HttpGet]   
        public ActionResult<List<User>> GetAllUsers()
        {
            return Ok(users);
        }
        public ActionResult<User> CreateUser(User user)
        {
            if(user == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetAllUsers), user.Id, user);
        }

    }
}
