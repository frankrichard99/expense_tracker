using ExpenseTracker.Models;

namespace ExpenseTracker.DTOs
{
    public class RegisterDto
    {
        
        public required string FirstName { set; get; }
        public required string LastName { set; get; }
        public required string Email { set; get; }
        public required string Password { set; get; }
        public required int Age { set; get; }
        
    }
}
